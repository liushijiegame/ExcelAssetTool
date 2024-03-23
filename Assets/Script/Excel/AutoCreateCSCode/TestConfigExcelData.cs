/*Auto Create, Don't Edit !!!*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class TestConfigExcelItem : ExcelItemBase
{
	/// <summary>
	/// 数据id
	/// </summary>>
	public int id;
	/// <summary>
	/// 字符串
	/// </summary>>
	public string testString;
	/// <summary>
	/// 字符串数组
	/// </summary>>
	public string[] testStringArray;
	/// <summary>
	/// 字符串二维数组
	/// </summary>>
	public StringArr[] testStringArray2;
	/// <summary>
	/// Int
	/// </summary>>
	public int testInt;
	/// <summary>
	/// Int数组
	/// </summary>>
	public int[] testIntArray;
	/// <summary>
	/// Int二维数组
	/// </summary>>
	public IntArr[] testIntArray2;
	/// <summary>
	/// Float
	/// </summary>>
	public float testFloat;
	/// <summary>
	/// Float数组
	/// </summary>>
	public float[] testFloatArray;
	/// <summary>
	/// Float二维数组
	/// </summary>>
	public FloatArr[] testFloatArray2;
	/// <summary>
	/// Bool
	/// </summary>>
	public bool testBool;
	/// <summary>
	/// Bool数组
	/// </summary>>
	public bool[] testBoolArray;
	/// <summary>
	/// Bool二维数组
	/// </summary>>
	public BoolArr[] testBoolArray2;
	/// <summary>
	/// Enum|枚举名(或枚举值)
	/// </summary>>
	public Space testEnum;
	/// <summary>
	/// Enum数组
	/// </summary>>
	public Space[] testEnumArray;
	/// <summary>
	/// Vector2
	/// </summary>>
	public Vector2 testVector2;
	/// <summary>
	/// Vector2数组
	/// </summary>>
	public Vector2[] testVector2Array;
	/// <summary>
	/// Vector2二维数组
	/// </summary>>
	public Vector2Arr[] testVector2Array2;
	/// <summary>
	/// Vector3
	/// </summary>>
	public Vector3 testVector3;
	/// <summary>
	/// Vector3数组
	/// </summary>>
	public Vector3[] testVector3Array;
	/// <summary>
	/// Vector3二维数组
	/// </summary>>
	public Vector3Arr[] testVector3Array2;
	/// <summary>
	/// Vector2Int
	/// </summary>>
	public Vector2Int testVector2Int;
	/// <summary>
	/// Vector2Int数组
	/// </summary>>
	public Vector2Int[] testVector2IntArray;
	/// <summary>
	/// Vector2Int二维数组
	/// </summary>>
	public Vector2IntArr[] testVector2IntArray2;
	/// <summary>
	/// Vector3Int
	/// </summary>>
	public Vector3Int testVector3Int;
	/// <summary>
	/// Vector3Int数组
	/// </summary>>
	public Vector3Int[] testVector3IntArray;
	/// <summary>
	/// Vector3Int二维数组
	/// </summary>>
	public Vector3IntArr[] testVector3IntArray2;
	/// <summary>
	/// Color
	/// </summary>>
	public Color testColor;
	/// <summary>
	/// Color数组
	/// </summary>>
	public Color[] testColorArray;
	/// <summary>
	/// Color二维数组
	/// </summary>>
	public ColorArr[] testColorArray2;
	/// <summary>
	/// Color32
	/// </summary>>
	public Color32 testColor32;
	/// <summary>
	/// Color32数组
	/// </summary>>
	public Color32[] testColor32Array;
	/// <summary>
	/// Color32二维数组
	/// </summary>>
	public Color32Arr[] testColor32Array2;
}


public class TestConfigExcelData : ExcelDataBase<TestConfigExcelItem>
{
	public TestConfigExcelItem[] items;

	public Dictionary<int,TestConfigExcelItem> itemDic = new Dictionary<int,TestConfigExcelItem>();

	public void Init()
	{
		itemDic.Clear();
		if(items != null && items.Length > 0)
		{
			for(int i = 0; i < items.Length; i++)
			{
				itemDic.Add(items[i].id, items[i]);
			}
		}
	}

	public TestConfigExcelItem GetTestConfigExcelItem(int id)
	{
		if(itemDic.ContainsKey(id))
			return itemDic[id];
		else
			return null;
	}
	#region --- Get Method ---

	public string GetTestString(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testString;
	}

	public string[] GetTestStringArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testStringArray;
	}
	public string GetTestStringArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testStringArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public StringArr[] GetTestStringArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testStringArray2;
	}
	public StringArr GetTestStringArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testStringArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public string GetTestStringArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testStringArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public int GetTestInt(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testInt;
	}

	public int[] GetTestIntArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testIntArray;
	}
	public int GetTestIntArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testIntArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public IntArr[] GetTestIntArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testIntArray2;
	}
	public IntArr GetTestIntArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testIntArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public int GetTestIntArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testIntArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public float GetTestFloat(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testFloat;
	}

	public float[] GetTestFloatArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testFloatArray;
	}
	public float GetTestFloatArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testFloatArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public FloatArr[] GetTestFloatArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testFloatArray2;
	}
	public FloatArr GetTestFloatArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testFloatArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public float GetTestFloatArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testFloatArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public bool GetTestBool(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testBool;
	}

	public bool[] GetTestBoolArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testBoolArray;
	}
	public bool GetTestBoolArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testBoolArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public BoolArr[] GetTestBoolArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testBoolArray2;
	}
	public BoolArr GetTestBoolArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testBoolArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public bool GetTestBoolArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testBoolArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public Space GetTestEnum(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testEnum;
	}

	public Space[] GetTestEnumArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testEnumArray;
	}
	public Space GetTestEnumArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testEnumArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public Vector2 GetTestVector2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector2;
	}

	public Vector2[] GetTestVector2Array(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector2Array;
	}
	public Vector2 GetTestVector2Array(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector2Array;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public Vector2Arr[] GetTestVector2Array2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector2Array2;
	}
	public Vector2Arr GetTestVector2Array2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector2Array2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public Vector2 GetTestVector2Array2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector2Array2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public Vector3 GetTestVector3(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector3;
	}

	public Vector3[] GetTestVector3Array(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector3Array;
	}
	public Vector3 GetTestVector3Array(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector3Array;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public Vector3Arr[] GetTestVector3Array2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector3Array2;
	}
	public Vector3Arr GetTestVector3Array2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector3Array2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public Vector3 GetTestVector3Array2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector3Array2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public Vector2Int GetTestVector2Int(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector2Int;
	}

	public Vector2Int[] GetTestVector2IntArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector2IntArray;
	}
	public Vector2Int GetTestVector2IntArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector2IntArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public Vector2IntArr[] GetTestVector2IntArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector2IntArray2;
	}
	public Vector2IntArr GetTestVector2IntArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector2IntArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public Vector2Int GetTestVector2IntArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector2IntArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public Vector3Int GetTestVector3Int(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector3Int;
	}

	public Vector3Int[] GetTestVector3IntArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector3IntArray;
	}
	public Vector3Int GetTestVector3IntArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector3IntArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public Vector3IntArr[] GetTestVector3IntArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testVector3IntArray2;
	}
	public Vector3IntArr GetTestVector3IntArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector3IntArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public Vector3Int GetTestVector3IntArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testVector3IntArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public Color GetTestColor(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testColor;
	}

	public Color[] GetTestColorArray(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testColorArray;
	}
	public Color GetTestColorArray(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testColorArray;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public ColorArr[] GetTestColorArray2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testColorArray2;
	}
	public ColorArr GetTestColorArray2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testColorArray2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public Color GetTestColorArray2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testColorArray2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	public Color32 GetTestColor32(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testColor32;
	}

	public Color32[] GetTestColor32Array(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testColor32Array;
	}
	public Color32 GetTestColor32Array(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testColor32Array;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}

	public Color32Arr[] GetTestColor32Array2(int id)
	{
		var item = GetTestConfigExcelItem(id);
		if(item == null)
			return default;
		return item.testColor32Array2;
	}
	public Color32Arr GetTestColor32Array2(int id, int index)
	{
		var item0 = GetTestConfigExcelItem (id);
		if(item0 == null)
			return default;
		var item1 = item0.testColor32Array2;
		if(item1 == null || index < 0 || index >= item1.Length)
			return default;
		return item1[index];
	}
	public Color32 GetTestColor32Array2(int id, int index1, int index2)
	{
		var item0 = GetTestConfigExcelItem(id);
		if(item0 == null)
			return default;
		var item1 = item0.testColor32Array2;
		if(item1 == null || index1 < 0 || index1 >= item1.Length)
			return default;
		var item2 = item1[index1];
		if(item2.array == null || index2 < 0 || index2 >= item2.array.Length)
			return default;
		return item2.array[index2];
	}

	#endregion
}


#if UNITY_EDITOR
public class TestConfigAssetAssignment
{
	public static bool CreateAsset(ExcelMediumData excelMediumData, string excelAssetPath)
	{
		var allRowItemDicList = excelMediumData.GetAllRowItemDicList();
		if(allRowItemDicList == null || allRowItemDicList.Count == 0)
			return false;

		int rowCount = allRowItemDicList.Count;
		TestConfigExcelData excelDataAsset = ScriptableObject.CreateInstance<TestConfigExcelData>();
		excelDataAsset.items = new TestConfigExcelItem[rowCount];

		for(int i = 0; i < rowCount; i++)
		{
			var itemRowDic = allRowItemDicList[i];
			excelDataAsset.items[i] = new TestConfigExcelItem();
			excelDataAsset.items[i].id = StringUtility.StringToInt(itemRowDic["id"]);
			excelDataAsset.items[i].testString = itemRowDic["testString"];
			excelDataAsset.items[i].testStringArray = StringUtility.StringToStringArray(itemRowDic["testStringArray"]);
			excelDataAsset.items[i].testStringArray2 = StringUtility.StringToStringArray2D(itemRowDic["testStringArray2"]);
			excelDataAsset.items[i].testInt = StringUtility.StringToInt(itemRowDic["testInt"]);
			excelDataAsset.items[i].testIntArray = StringUtility.StringToIntArray(itemRowDic["testIntArray"]);
			excelDataAsset.items[i].testIntArray2 = StringUtility.StringToIntArray2D(itemRowDic["testIntArray2"]);
			excelDataAsset.items[i].testFloat = StringUtility.StringToFloat(itemRowDic["testFloat"]);
			excelDataAsset.items[i].testFloatArray = StringUtility.StringToFloatArray(itemRowDic["testFloatArray"]);
			excelDataAsset.items[i].testFloatArray2 = StringUtility.StringToFloatArray2D(itemRowDic["testFloatArray2"]);
			excelDataAsset.items[i].testBool = StringUtility.StringToBool(itemRowDic["testBool"]);
			excelDataAsset.items[i].testBoolArray = StringUtility.StringToBoolArray(itemRowDic["testBoolArray"]);
			excelDataAsset.items[i].testBoolArray2 = StringUtility.StringToBoolArray2D(itemRowDic["testBoolArray2"]);
			excelDataAsset.items[i].testEnum = StringUtility.StringToEnum<Space>(itemRowDic["testEnum"]);
			excelDataAsset.items[i].testEnumArray = StringUtility.StringToEnumArray<Space>(itemRowDic["testEnumArray"]);
			excelDataAsset.items[i].testVector2 = StringUtility.StringToVector2(itemRowDic["testVector2"]);
			excelDataAsset.items[i].testVector2Array = StringUtility.StringToVector2Array(itemRowDic["testVector2Array"]);
			excelDataAsset.items[i].testVector2Array2 = StringUtility.StringToVector2Array2D(itemRowDic["testVector2Array2"]);
			excelDataAsset.items[i].testVector3 = StringUtility.StringToVector3(itemRowDic["testVector3"]);
			excelDataAsset.items[i].testVector3Array = StringUtility.StringToVector3Array(itemRowDic["testVector3Array"]);
			excelDataAsset.items[i].testVector3Array2 = StringUtility.StringToVector3Array2D(itemRowDic["testVector3Array2"]);
			excelDataAsset.items[i].testVector2Int = StringUtility.StringToVector2Int(itemRowDic["testVector2Int"]);
			excelDataAsset.items[i].testVector2IntArray = StringUtility.StringToVector2IntArray(itemRowDic["testVector2IntArray"]);
			excelDataAsset.items[i].testVector2IntArray2 = StringUtility.StringToVector2IntArray2D(itemRowDic["testVector2IntArray2"]);
			excelDataAsset.items[i].testVector3Int = StringUtility.StringToVector3Int(itemRowDic["testVector3Int"]);
			excelDataAsset.items[i].testVector3IntArray = StringUtility.StringToVector3IntArray(itemRowDic["testVector3IntArray"]);
			excelDataAsset.items[i].testVector3IntArray2 = StringUtility.StringToVector3IntArray2D(itemRowDic["testVector3IntArray2"]);
			excelDataAsset.items[i].testColor = StringUtility.StringToColor(itemRowDic["testColor"]);
			excelDataAsset.items[i].testColorArray = StringUtility.StringToColorArray(itemRowDic["testColorArray"]);
			excelDataAsset.items[i].testColorArray2 = StringUtility.StringToColorArray2D(itemRowDic["testColorArray2"]);
			excelDataAsset.items[i].testColor32 = StringUtility.StringToColor32(itemRowDic["testColor32"]);
			excelDataAsset.items[i].testColor32Array = StringUtility.StringToColor32Array(itemRowDic["testColor32Array"]);
			excelDataAsset.items[i].testColor32Array2 = StringUtility.StringToColor32Array2D(itemRowDic["testColor32Array2"]);
		}
		if(!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string fullPath = Path.Combine(excelAssetPath,typeof(TestConfigExcelData).Name) + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(fullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset,fullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif



