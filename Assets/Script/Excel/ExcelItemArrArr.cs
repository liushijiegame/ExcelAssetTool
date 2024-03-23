//**************************************************
//* FileName:          ExcelItemArrArr.cs
//* Author:            ZCS
//* Version:           1.0
//* CreateTime:        2021/05/15 23:53:34
//**************************************************

using System;
using UnityEngine;

[Serializable]
public struct StringArr
{
    public string[] array;
}
[Serializable]
public struct IntArr
{
    public int[] array;
}
[Serializable]
public struct FloatArr
{
    public float[] array;
}
[Serializable]
public struct BoolArr
{
    public bool[] array;
}

[Serializable]
public struct Vector2Arr
{
    public Vector2[] array;
}
[Serializable]
public struct Vector3Arr
{
    public Vector3[] array;
}
[Serializable]
public struct Vector2IntArr
{
    public Vector2Int[] array;
}
[Serializable]
public struct Vector3IntArr
{
    public Vector3Int[] array;
}
[Serializable]
public struct ColorArr
{
    public Color[] array;
}
[Serializable]
public struct Color32Arr
{
    public Color32[] array;
}

////不支持泛型枚举序列化
//[Serializable]
//public struct EnumArr<T> where T : Enum
//{
//    public T[] array;
//}