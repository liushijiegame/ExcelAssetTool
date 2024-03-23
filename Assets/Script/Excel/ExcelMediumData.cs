using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Excel中间数据
public class ExcelMediumData
{
    //Excel名字
    public string excelName;

    //Excel是否有效
    public bool isValid = false;
    //是否生成CSharp数据类
    public bool isCreateCSharp = false;
    //是否生成Asset创建类
    public bool isCreateAssignment = false;
    //是否生成Asset文件
    public bool isCreateAsset = false;

    //数据注释
    public string[] propertyNodeArray = null;
    //数据名称
    public string[] propertyNameArray = null;
    //数据类型
    public string[] propertyTypeArray = null;
    //List<每行数据内容>
    public List<string[]> allRowItemList = new List<string[]>();

    //注释行号
    public List<int> annotationRowList = new List<int>();
    //注释列号
    public List<int> annotationColList = new List<int>();

    //List<每行数据>，List<Dictionary<单元格字段名称, 单元格字段值>>
    public List<Dictionary<string,string>> GetAllRowItemDicList()
    {
        if(propertyNameArray == null || propertyNameArray.Length == 0)
            return null;
        if(allRowItemList.Count == 0)
            return null;

        List<Dictionary<string,string>> allRowItemDicList = new List<Dictionary<string,string>>(allRowItemList.Count);

        for(int i = 0; i < allRowItemList.Count; i++)
        {
            string[] rowArray = allRowItemList[i];
            //跳过空数据
            if(rowArray == null || rowArray.Length == 0)
                continue;
            //跳过注释数据
            if(annotationRowList.Contains(i))
                continue;

            //每行数据，对应字段名称和字段值
            Dictionary<string,string> rowDic = new Dictionary<string,string>();
            for(int j = 0; j < propertyNameArray.Length; j++)
            {
                //跳过注释字段
                if(annotationColList.Contains(j))
                    continue;

                string propertyName = propertyNameArray[j];
                string propertyValue = j < rowArray.Length ? rowArray[j] : null;
                rowDic[propertyName] = propertyValue;
            }
            allRowItemDicList.Add(rowDic);
        }
        return allRowItemDicList;
    }

}
