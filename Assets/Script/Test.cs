using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Text value;
    private TestConfigExcelData _testConfigExcelData;
    void Start()
    {
        var maskBtn = this.transform.Find("Button").GetComponent<Button>();
        maskBtn.onClick.AddListener(ChangeValue);
        value = this.transform.Find("Text").GetComponent<Text>();
        _testConfigExcelData = Resources.Load<TestConfigExcelData>("ExcelAsset/TestConfigExcelData");
    }

    void ChangeValue()
    {

        value.text = _testConfigExcelData.items[1].testString;
    }

}
