using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Reflection;
using System;
using System.Linq;

public class ExcelDataReader
{
    //Excel第1行对应特殊标记
    private const int specialSignRow = 0;
    //Excel第2行对应中文说明
    private const int excelNodeRow = 1;
    //Excel第3行对应字段名称
    private const int excelNameRow = 2;
    //Excel第4行对应字段类型
    private const int excelTypeRow = 3;
    //Excel第5行及以后对应字段值
    private const int excelDataRow = 4;

    //标记注释行/列
    private const string annotationSign = "//";

    #region --- Read Excel ---

    //创建Excel对应的C#类
    public static void ReadAllExcelToCode(string allExcelPath,string codeSavePath)
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        string[] excelFileFullPaths = Directory.GetFiles(allExcelPath,"*.xlsx");
        if(excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //遍历所有Excel，创建C#类
        for(int i = 0; i < excelFileFullPaths.Length; i++)
        {
            ReadOneExcelToCode(excelFileFullPaths[i],codeSavePath);
        }
    }

    //创建Excel对应的C#类
    public static void ReadOneExcelToCode(string excelFullPath,string codeSavePath)
    {
        //解析Excel获取中间数据
        ExcelMediumData excelMediumData = CreateClassCodeByExcelPath(excelFullPath);
        if(excelMediumData == null)
        {
            Debug.LogError($"读取Excel失败 : {excelFullPath}");
            return;
        }
        if(!excelMediumData.isValid)
        {
            Debug.LogError($"读取Excel失败，Excel标记失效 : {excelMediumData.excelName}");
            return;
        }

        if(!excelMediumData.isCreateCSharp && !excelMediumData.isCreateAssignment)
        {
            Debug.LogError($"读取Excel失败，Excel不允许生成CSCode : {excelMediumData.excelName}");
            return;
        }

        //根据数据生成C#脚本
        string classCodeStr = ExcelCodeCreater.CreateCodeStrByExcelData(excelMediumData);
        if(string.IsNullOrEmpty(classCodeStr))
        {
            Debug.LogError($"解析Excel失败 : {excelMediumData.excelName}");
            return;
        }

        //检查导出路径
        if(!Directory.Exists(codeSavePath))
            Directory.CreateDirectory(codeSavePath);
        //类名
        string codeFileName = excelMediumData.excelName + "ExcelData";
        //写文件，生成CS类文件
        StreamWriter sw = new StreamWriter($"{codeSavePath}/{codeFileName}.cs");
        sw.WriteLine(classCodeStr);
        sw.Close();
        //
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        //
        Debug.Log($"生成Excel的CS成功 : {excelMediumData.excelName}");
    }

    #endregion

    #region --- Create Asset ---

    //创建Excel对应的Asset数据文件
    public static void CreateAllExcelAsset(string allExcelPath,string assetSavePath)
    {
        //读取所有Excel文件
        //指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。
        string[] excelFileFullPaths = Directory.GetFiles(allExcelPath,"*.xlsx");
        if(excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.Log("Excel file count == 0");
            return;
        }
        //遍历所有Excel，创建Asset
        for(int i = 0; i < excelFileFullPaths.Length; i++)
        {
            CreateOneExcelAsset(excelFileFullPaths[i],assetSavePath);
        }
    }

    //创建Excel对应的Asset数据文件
    public static void CreateOneExcelAsset(string excelFullPath,string assetSavePath)
    {
        //解析Excel获取中间数据
        ExcelMediumData excelMediumData = CreateClassCodeByExcelPath(excelFullPath);
        if(excelMediumData == null)
        {
            Debug.LogError($"读取Excel失败 : {excelFullPath}");
            return;
        }
        if(!excelMediumData.isValid)
        {
            Debug.LogError($"读取Excel失败，Excel标记失效 : {excelMediumData.excelName}");
            return;
        }

        if(!excelMediumData.isCreateAsset)
        {
            Debug.LogError($"读取Excel失败，Excel不允许生成Asset : {excelMediumData.excelName}");
            return;
        }

        ////获取当前程序集
        //Assembly assembly = Assembly.GetExecutingAssembly();
        ////创建类的实例，返回为 object 类型，需要强制类型转换，assembly.CreateInstance("类的完全限定名（即包括命名空间）");
        //object class0bj = assembly.CreateInstance(excelMediumData.excelName + "Assignment",true);

        //必须遍历所有程序集来获得类型。当前在Assembly-CSharp-Editor中，目标类型在Assembly-CSharp中，不同程序将无法获取类型
        Type assignmentType = null;
        string assetAssignmentName = excelMediumData.excelName + "AssetAssignment";
        foreach(var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            //查找目标类型
            Type tempType = asm.GetType(assetAssignmentName);
            if(tempType != null)
            {
                assignmentType = tempType;
                break;
            }
        }
        if(assignmentType == null)
        {
            Debug.LogError($"创界Asset失败，未找到Asset生成类 : {excelMediumData.excelName}");
            return;
        }

        //反射获取方法
        MethodInfo methodInfo = assignmentType.GetMethod("CreateAsset");
        if(methodInfo == null)
        {
            if(assignmentType == null)
            {
                Debug.LogError($"创界Asset失败，未找到Asset创建函数 : {excelMediumData.excelName}");
                return;
            }
        }

        methodInfo.Invoke(null,new object[] { excelMediumData,assetSavePath });
        //创建Asset文件成功
        Debug.Log($"生成Excel的Asset成功 : {excelMediumData.excelName}");
    }

    #endregion

    #region --- private ---

    //解析Excel，创建中间数据
    private static ExcelMediumData CreateClassCodeByExcelPath(string excelFileFullPath)
    {
        if(string.IsNullOrEmpty(excelFileFullPath))
            return null;

        excelFileFullPath = excelFileFullPath.Replace("\\","/");
        //读取Excel
        FileStream stream = File.Open(excelFileFullPath,FileMode.Open,FileAccess.Read);
        if(stream == null)
            return null;
        //解析Excel
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //无效Excel
        if(excelReader == null || !excelReader.IsValid)
        {
            Debug.Log("Invalid excel ： " + excelFileFullPath);
            return null;
        }

        Debug.Log("开始解析Excel : " + excelReader.Name);

        //记录Excel数据
        ExcelMediumData excelMediumData = new ExcelMediumData();

        //Excel名字
        excelMediumData.excelName = excelReader.Name;

        //当前遍历的行
        int curRowIndex = 0;
        //开始读取，按行遍历
        while(excelReader.Read())
        {
            //这一行没有读取到数据，视为无效行数据
            if(excelReader.FieldCount <= 0)
            {
                curRowIndex++;
                continue;
            }
            //读取每一行的完整数据
            string[] datas = new string[excelReader.FieldCount];
            for(int j = 0; j < excelReader.FieldCount; ++j)
            {
                //可以直接读取指定类型数据，不过只支持有限数据类型，这里统一读取string，然后再数据转化
                //excelReader.GetInt32(j); excelReader.GetFloat(j);

                //读取每一个单元格数据
                datas[j] = excelReader.GetString(j);
            }

            switch(curRowIndex)
            {
                case specialSignRow:
                    //特殊标记行
                    string specialSignStr = datas[0];
                    if(specialSignStr.Length >= 4)
                    {
                        excelMediumData.isValid = specialSignStr[0] == 'T';
                        excelMediumData.isCreateCSharp = specialSignStr[1] == 'T';
                        excelMediumData.isCreateAssignment = specialSignStr[2] == 'T';
                        excelMediumData.isCreateAsset = specialSignStr[3] == 'T';
                    }
                    else
                    {
                        Debug.LogError("未解析到特殊标记");
                    }
                    break;
                case excelNodeRow:
                    //数据注释行
                    excelMediumData.propertyNodeArray = datas;
                    break;
                case excelNameRow:
                    //数据名称行
                    excelMediumData.propertyNameArray = datas;
                    //注释列号
                    for(int i = 0; i < datas.Length; i++)
                    {
                        if(string.IsNullOrEmpty(datas[i]) || datas[i].StartsWith(annotationSign))
                            excelMediumData.annotationColList.Add(i);
                    }
                    break;
                case excelTypeRow:
                    //数据类型行
                    excelMediumData.propertyTypeArray = datas;
                    break;
                default:
                    //数据内容行
                    excelMediumData.allRowItemList.Add(datas);
                    //注释行号
                    if(string.IsNullOrEmpty(datas[0]) || datas[0].StartsWith(annotationSign))
                        excelMediumData.annotationRowList.Add(excelMediumData.allRowItemList.Count - 1);
                    break;
            }
            //
            curRowIndex++;
        }

        if(CheckExcelMediumData(ref excelMediumData))
        {
            Debug.Log("读取Excel成功");
            return excelMediumData;
        }
        else
        {
            Debug.LogError("读取Excel失败");
            return null;
        }
    }

    //校验Excel数据
    private static bool CheckExcelMediumData(ref ExcelMediumData mediumData)
    {
        if(mediumData == null)
            return false;

        //检查数据有效性

        if(!mediumData.isValid)
        {
            Debug.LogError("Excel被标记无效");
            return false;
        }

        if(string.IsNullOrEmpty(mediumData.excelName))
        {
            Debug.LogError("Excel名字为空");
            return false;
        }

        if(mediumData.propertyNameArray == null || mediumData.propertyNameArray.Length == 0)
        {
            Debug.LogError("未解析到数据名称");
            return false;
        }
        if(mediumData.propertyTypeArray == null || mediumData.propertyTypeArray.Length == 0)
        {
            Debug.LogError("未解析到数据类型");
            return false;
        }
        if(mediumData.propertyNameArray.Length != mediumData.propertyTypeArray.Length)
        {
            Debug.LogError("数据名称与数据类型数量不一致");
            return false;
        }
        if(mediumData.allRowItemList.Count == 0)
        {
            Debug.LogError("数据内容为空");
            return false;
        }

        if(mediumData.propertyNameArray[0] != "id")
        {
            Debug.LogError("第一个字段必须是id字段");
            return false;
        }

        return true;
    }

    #endregion

}