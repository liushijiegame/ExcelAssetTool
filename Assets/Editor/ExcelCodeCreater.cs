using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using System;

public class ExcelCodeCreater
{

    //创建代码，生成数据C#类
    public static string CreateCodeStrByExcelData(ExcelMediumData excelMediumData)
    {
        if(excelMediumData == null)
            return null;

        //行数据类名
        string itemClassName = excelMediumData.excelName + "ExcelItem";
        //整体数据类名
        string dataClassName = excelMediumData.excelName + "ExcelData";

        //开始生成类
        StringBuilder classSource = new StringBuilder();
        classSource.AppendLine("/*Auto Create, Don't Edit !!!*/");
        classSource.AppendLine();
        //添加引用
        classSource.AppendLine("using UnityEngine;");
        classSource.AppendLine("using System.Collections.Generic;");
        classSource.AppendLine("using System;");
        classSource.AppendLine("using System.IO;");
        classSource.AppendLine();
        //生成CSharp数据类
        if(excelMediumData.isCreateCSharp)
        {
            //生成行数据类，记录每行数据
            classSource.AppendLine(CreateExcelRowItemClass(itemClassName,excelMediumData));
            classSource.AppendLine();
            //生成整体数据类，记录整个Excel的所有行数据
            classSource.AppendLine(CreateExcelAllDataClass(dataClassName,itemClassName,excelMediumData));
            classSource.AppendLine();
        }
        //生成Asset创建类
        if(excelMediumData.isCreateAssignment)
        {
            //生成Asset操作类，用于自动创建Excel对应的Asset文件并赋值
            classSource.AppendLine(CreateExcelAssetClass(excelMediumData));
            classSource.AppendLine();
        }
        //
        return classSource.ToString();
    }

    //----------

    //生成行数据类
    private static string CreateExcelRowItemClass(string itemClassName,ExcelMediumData excelMediumData)
    {
        //生成Excel行数据类
        StringBuilder classSource = new StringBuilder();
        //类名
        classSource.AppendLine("[Serializable]");
        classSource.AppendLine($"public class {itemClassName} : ExcelItemBase");
        classSource.AppendLine("{");
        //声明所有字段
        for(int i = 0; i < excelMediumData.propertyNameArray.Length; i++)
        {
            //跳过注释字段
            if(excelMediumData.annotationColList.Contains(i))
                continue;

            //添加注释
            if(i < excelMediumData.propertyNodeArray.Length)
            {
                string propertyNode = excelMediumData.propertyNodeArray[i];
                if(!string.IsNullOrEmpty(propertyNode))
                {
                    classSource.AppendLine("\t/// <summary>");
                    classSource.AppendLine($"\t/// {propertyNode}");
                    classSource.AppendLine("\t/// </summary>>");
                }
            }

            //声明行数据类的字段
            string propertyName = excelMediumData.propertyNameArray[i];
            string propertyType = excelMediumData.propertyTypeArray[i];
            string typeStr = GetPropertyType(propertyType);
            classSource.AppendLine($"\tpublic {typeStr} {propertyName};");
        }
        classSource.AppendLine("}");
        return classSource.ToString();
    }

    //----------

    //生成整体数据类
    private static string CreateExcelAllDataClass(string dataClassName,string itemClassName,ExcelMediumData excelMediumData)
    {
        StringBuilder classSource = new StringBuilder();
        //类名
        classSource.AppendLine($"public class {dataClassName} : ExcelDataBase<{itemClassName}>");
        classSource.AppendLine("{");
        //声明字段，行数据类数组
        classSource.AppendLine($"\tpublic {itemClassName}[] items;");
        classSource.AppendLine();
        //id字段类型
        string idTypeStr = GetPropertyType(excelMediumData.propertyTypeArray[0]);
        //声明字典
        classSource.AppendLine($"\tpublic Dictionary<{idTypeStr},{itemClassName}> itemDic = new Dictionary<{idTypeStr},{itemClassName}>();");
        classSource.AppendLine();
        //字段初始化方法
        classSource.AppendLine("\tpublic void Init()");
        classSource.AppendLine("\t{");
        classSource.AppendLine("\t\titemDic.Clear();");
        classSource.AppendLine("\t\tif(items != null && items.Length > 0)");
        classSource.AppendLine("\t\t{");
        classSource.AppendLine("\t\t\tfor(int i = 0; i < items.Length; i++)");
        classSource.AppendLine("\t\t\t{");
        classSource.AppendLine("\t\t\t\titemDic.Add(items[i].id, items[i]);");
        classSource.AppendLine("\t\t\t}");
        classSource.AppendLine("\t\t}");
        classSource.AppendLine("\t}");
        classSource.AppendLine();
        //字典获取方法
        classSource.AppendLine($"\tpublic {itemClassName} Get{itemClassName}({idTypeStr} id)");
        classSource.AppendLine("\t{");
        classSource.AppendLine("\t\tif(itemDic.ContainsKey(id))");
        classSource.AppendLine("\t\t\treturn itemDic[id];");
        classSource.AppendLine("\t\telse");
        classSource.AppendLine("\t\t\treturn null;");
        classSource.AppendLine("\t}");

        //每个字段Get函数
        classSource.AppendLine("\t#region --- Get Method ---");
        classSource.AppendLine();

        for(int i = 1; i < excelMediumData.propertyNameArray.Length; i++)
        {
            if(excelMediumData.annotationColList.Contains(i))
                continue;
            string propertyName = excelMediumData.propertyNameArray[i];
            string propertyType = excelMediumData.propertyTypeArray[i];
            //每个字段Get函数
            classSource.AppendLine(CreateCodePropertyMethod(itemClassName,idTypeStr,propertyName,propertyType));
        }
        classSource.AppendLine("\t#endregion");
        classSource.AppendLine("}");
        return classSource.ToString();
    }

    //生成数据字段对应Get方法
    private static string CreateCodePropertyMethod(string itemClassName,string idTypeStr,string propertyName,string propertyType)
    {
        StringBuilder methodBuilder = new StringBuilder();
        string itemNameStr = propertyName.FirstOrDefault().ToString().ToUpper() + propertyName.Substring(1);
        string itemTypeStr = GetPropertyType(propertyType);
        //字段Get函数
        methodBuilder.AppendLine($"\tpublic {itemTypeStr} Get{itemNameStr}({idTypeStr} id)");
        methodBuilder.AppendLine("\t{");
        methodBuilder.AppendLine($"\t\tvar item = Get{itemClassName}(id);");
        methodBuilder.AppendLine("\t\tif(item == null)");
        methodBuilder.AppendLine("\t\t\treturn default;");
        methodBuilder.AppendLine($"\t\treturn item.{propertyName};");
        methodBuilder.AppendLine("\t}");
        //如果是一维数组
        if(propertyType.Contains("[]"))
        {
            //typeStr:int[]或IntArr[] ,返回值:int或IntArr
            //string itemTypeStr1d = GetPropertyType(propertyType.Replace("[]",""));
            string itemTypeStr1d = itemTypeStr.Replace("[]","");
            methodBuilder.AppendLine($"\tpublic {itemTypeStr1d} Get{itemNameStr}({idTypeStr} id, int index)");
            methodBuilder.AppendLine("\t{");
            methodBuilder.AppendLine($"\t\tvar item0 = Get{itemClassName} (id);");
            methodBuilder.AppendLine("\t\tif(item0 == null)");
            methodBuilder.AppendLine("\t\t\treturn default;");
            methodBuilder.AppendLine($"\t\tvar item1 = item0.{propertyName};");
            methodBuilder.AppendLine("\t\tif(item1 == null || index < 0 || index >= item1.Length)");
            methodBuilder.AppendLine("\t\t\treturn default;");
            methodBuilder.AppendLine("\t\treturn item1[index];");
            methodBuilder.AppendLine("\t}");
        }
        //如果是二维数组
        if(propertyType.Contains("[][]"))
        {
            //propertyType:int[][], 返回值:int
            string itemTypeStr1d = GetPropertyType(propertyType.Replace("[][]",""));
            methodBuilder.AppendLine($"\tpublic {itemTypeStr1d} Get{itemNameStr}({idTypeStr} id, int index1, int index2)");
            methodBuilder.AppendLine("\t{");
            methodBuilder.AppendLine($"\t\tvar item0 = Get{itemClassName}(id);");
            methodBuilder.AppendLine("\t\tif(item0 == null)");
            methodBuilder.AppendLine("\t\t\treturn default;");
            methodBuilder.AppendLine($"\t\tvar item1 = item0.{propertyName};");
            methodBuilder.AppendLine("\t\tif(item1 == null || index1 < 0 || index1 >= item1.Length)");
            methodBuilder.AppendLine("\t\t\treturn default;");
            methodBuilder.AppendLine("\t\tvar item2 = item1[index1];");
            methodBuilder.AppendLine("\t\tif(item2.array == null || index2 < 0 || index2 >= item2.array.Length)");
            methodBuilder.AppendLine("\t\t\treturn default;");
            methodBuilder.AppendLine("\t\treturn item2.array[index2];");
            methodBuilder.AppendLine("\t}");
        }
        //
        return methodBuilder.ToString();
    }

    //----------

    //生成Asset创建类
    private static string CreateExcelAssetClass(ExcelMediumData excelMediumData)
    {
        string itemClassName = excelMediumData.excelName + "ExcelItem";
        string dataClassName = excelMediumData.excelName + "ExcelData";
        string assignmentClassName = excelMediumData.excelName + "AssetAssignment";

        StringBuilder classSource = new StringBuilder();
        classSource.AppendLine("#if UNITY_EDITOR");
        //类名
        classSource.AppendLine($"public class {assignmentClassName}");
        classSource.AppendLine("{");
        //方法名
        classSource.AppendLine("\tpublic static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)");
        //方法体，若有需要可加入try/catch
        classSource.AppendLine("\t{");
        classSource.AppendLine("\t\tvar allRowItemDicList = excelMediumData.GetAllRowItemDicList();");
        classSource.AppendLine("\t\tif(allRowItemDicList == null || allRowItemDicList.Count == 0)");
        classSource.AppendLine("\t\t\treturn false;");
        classSource.AppendLine();
        classSource.AppendLine("\t\tint rowCount = allRowItemDicList.Count;");
        classSource.AppendLine($"\t\t{dataClassName} excelDataAsset = ScriptableObject.CreateInstance<{dataClassName}>();");
        classSource.AppendLine($"\t\texcelDataAsset.items = new {itemClassName}[rowCount];");
        classSource.AppendLine();
        classSource.AppendLine("\t\tfor(int i = 0; i < rowCount; i++)");
        classSource.AppendLine("\t\t{");
        classSource.AppendLine("\t\t\tvar itemRowDic = allRowItemDicList[i];");
        classSource.AppendLine($"\t\t\texcelDataAsset.items[i] = new {itemClassName}();");

        for(int i = 0; i < excelMediumData.propertyNameArray.Length; i++)
        {
            if(excelMediumData.annotationColList.Contains(i))
                continue;
            string propertyName = excelMediumData.propertyNameArray[i];
            string propertyType = excelMediumData.propertyTypeArray[i];
            classSource.Append($"\t\t\texcelDataAsset.items[i].{propertyName} = ");
            classSource.Append(AssignmentCodeProperty(propertyName,propertyType));
            classSource.AppendLine(";");
        }
        classSource.AppendLine("\t\t}");
        classSource.AppendLine("\t\tif(!Directory.Exists(excelAssetPath))");
        classSource.AppendLine("\t\t\tDirectory.CreateDirectory(excelAssetPath);");
        classSource.AppendLine($"\t\tstring fullPath = Path.Combine(excelAssetPath,typeof({dataClassName}).Name) + \".asset\";");
        classSource.AppendLine("\t\tUnityEditor.AssetDatabase.DeleteAsset(fullPath);");
        classSource.AppendLine("\t\tUnityEditor.AssetDatabase.CreateAsset(excelDataAsset,fullPath);");
        classSource.AppendLine("\t\tUnityEditor.AssetDatabase.Refresh();");
        classSource.AppendLine("\t\treturn true;");
        classSource.AppendLine("\t}");
        //          
        classSource.AppendLine("}");
        classSource.AppendLine("#endif");
        return classSource.ToString();
    }

    //声明Asset操作类字段
    private static string AssignmentCodeProperty(string propertyName,string propertyType)
    {
        string stringValue = $"itemRowDic[\"{propertyName}\"]";
        string typeStr = GetPropertyType(propertyType);
        switch(typeStr)
        {
            //字段
            case "int":
                return "StringUtility.StringToInt(" + stringValue + ")";
            case "float":
                return "StringUtility.StringToFloat(" + stringValue + ")";
            case "bool":
                return "StringUtility.StringToBool(" + stringValue + ")";
            case "Vector2":
                return "StringUtility.StringToVector2(" + stringValue + ")";
            case "Vector3":
                return "StringUtility.StringToVector3(" + stringValue + ")";
            case "Vector2Int":
                return "StringUtility.StringToVector2Int(" + stringValue + ")";
            case "Vector3Int":
                return "StringUtility.StringToVector3Int(" + stringValue + ")";
            case "Color":
                return "StringUtility.StringToColor(" + stringValue + ")";
            case "Color32":
                return "StringUtility.StringToColor32(" + stringValue + ")";
            case "string":
                return stringValue;
            //一维
            case "int[]":
                return "StringUtility.StringToIntArray(" + stringValue + ")";
            case "float[]":
                return "StringUtility.StringToFloatArray(" + stringValue + ")";
            case "bool[]":
                return "StringUtility.StringToBoolArray(" + stringValue + ")";
            case "Vector2[]":
                return "StringUtility.StringToVector2Array(" + stringValue + ")";
            case "Vector3[]":
                return "StringUtility.StringToVector3Array(" + stringValue + ")";
            case "Vector2Int[]":
                return "StringUtility.StringToVector2IntArray(" + stringValue + ")";
            case "Vector3Int[]":
                return "StringUtility.StringToVector3IntArray(" + stringValue + ")";
            case "Color[]":
                return "StringUtility.StringToColorArray(" + stringValue + ")";
            case "Color32[]":
                return "StringUtility.StringToColor32Array(" + stringValue + ")";
            case "string[]":
                return "StringUtility.StringToStringArray(" + stringValue + ")";
            //二维
            case "IntArr[]":
                return "StringUtility.StringToIntArray2D(" + stringValue + ")";
            case "FloatArr[]":
                return "StringUtility.StringToFloatArray2D(" + stringValue + ")";
            case "BoolArr[]":
                return "StringUtility.StringToBoolArray2D(" + stringValue + ")";
            case "Vector2Arr[]":
                return "StringUtility.StringToVector2Array2D(" + stringValue + ")";
            case "Vector3Arr[]":
                return "StringUtility.StringToVector3Array2D(" + stringValue + ")";
            case "Vector2IntArr[]":
                return "StringUtility.StringToVector2IntArray2D(" + stringValue + ")";
            case "Vector3IntArr[]":
                return "StringUtility.StringToVector3IntArray2D(" + stringValue + ")";
            case "ColorArr[]":
                return "StringUtility.StringToColorArray2D(" + stringValue + ")";
            case "Color32Arr[]":
                return "StringUtility.StringToColor32Array2D(" + stringValue + ")";
            case "StringArr[]":
                return "StringUtility.StringToStringArray2D(" + stringValue + ")";
            default:
                //枚举
                if(propertyType.StartsWith("enum"))
                {
                    string enumType = propertyType.Split('|').FirstOrDefault();
                    string enumName = propertyType.Split('|').LastOrDefault();
                    if(enumType == "enum")
                        return "StringUtility.StringToEnum<" + enumName + ">(" + stringValue + ")";
                    else if(enumType == "enum[]")
                        return "StringUtility.StringToEnumArray<" + enumName + ">(" + stringValue + ")";
                    else if(enumType == "enum[][]")
                        return "StringUtility.StringToEnumArray2D<" + enumName + ">(" + stringValue + ")";
                }
                return stringValue;
        }
    }

    //判断字段类型
    private static string GetPropertyType(string propertyType)
    {
        string lowerType = propertyType.ToLower();
        switch(lowerType)
        {
            case "int":
                return "int";
            case "int[]":
                return "int[]";
            case "int[][]":
                return "IntArr[]";
            case "float":
                return "float";
            case "float[]":
                return "float[]";
            case "float[][]":
                return "FloatArr[]";
            case "bool":
                return "bool";
            case "bool[]":
                return "bool[]";
            case "bool[][]":
                return "BoolArr[]";
            case "string":
                return "string";
            case "string[]":
                return "string[]";
            case "string[][]":
                return "StringArr[]";

            case "vector2":
                return "Vector2";
            case "vector2[]":
                return "Vector2[]";
            case "vector2[][]":
                return "Vector2Arr[]";
            case "vector2int":
                return "Vector2Int";
            case "vector2int[]":
                return "Vector2Int[]";
            case "vector2int[][]":
                return "Vector2IntArr[]";

            case "vector3":
                return "Vector3";
            case "vector3[]":
                return "Vector3[]";
            case "vector3[][]":
                return "Vector3Arr[]";
            case "vector3int":
                return "Vector3Int";
            case "vector3int[]":
                return "Vector3Int[]";
            case "vector3int[][]":
                return "Vector3IntArr[]";

            case "color":
                return "Color";
            case "color[]":
                return "Color[]";
            case "color[][]":
                return "ColorArr[]";
            case "color32":
                return "Color32";
            case "color32[]":
                return "Color32[]";
            case "color32[][]":
                return "Color32Arr[]";

            default:
                if(propertyType.StartsWith("enum"))
                {
                    string enumType = propertyType.Split('|').FirstOrDefault();
                    string enumName = propertyType.Split('|').LastOrDefault();
                    switch(enumType)
                    {
                        case "enum":
                            return enumName;
                        case "enum[]":
                            return $"{enumName}[]";
                        case "enum[][]":
                            return $"EnumArr<{enumName}>[]";
                        default:
                            break;
                    }
                }
                return "string";
        }
    }

}
