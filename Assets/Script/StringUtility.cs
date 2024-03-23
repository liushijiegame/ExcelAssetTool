using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;

public static class StringUtility
{

    #region --- AddColor ---

    public static string AddColor(object obj,Color color)
    {
        return AddColor(obj,color);
    }
    public static string AddColor(this string str,Color color)
    {
        //把颜色转换为16进制字符串，添加到富文本
        return string.Format("<color=#{0}>{1}</color>",ColorUtility.ToHtmlStringRGBA(color),str);
    }
    public static string AddColor(string str1,string str2,Color color)
    {
        return AddColor(str1 + str2,color);
    }
    public static string AddColor(string str1,string str2,string str3,Color color)
    {
        return AddColor(str1 + str2 + str3,color);
    }

    #endregion

    #region --- string length ---

    /// <summary>
    /// 化简字符串长度
    /// </summary>
    /// <param name="targetStr"></param>
    /// <param name="targetLength">目标长度，英文字符==1，中文字符==2</param>
    /// <returns></returns>
    public static string AbbrevStringWithinLength(string targetStr,int targetLength,string abbrevPostfix)
    {
        //C#实际统计：一个中文字符长度==1，英文字符长度==1
        //UI显示要求：一个中文字符长度==2，英文字符长度==1

        //校验参数
        if(string.IsNullOrEmpty(targetStr) || targetLength <= 0)
            return targetStr;
        //字符串长度 * 2 <= 目标长度，即使是全中文也在长度范围内
        if(targetStr.Length * 2 <= targetLength)
            return targetStr;
        //遍历字符
        char[] chars = targetStr.ToCharArray();
        int curLen = 0;
        for(int i = 0; i < chars.Length; i++)
        {
            //累加字符串长度
            if(chars[i] >= 0x4e00 && chars[i] <= 0x9fbb)
                curLen += 2;
            else
                curLen += 1;

            //如果当前位置累计长度超过目标长度，取0~i-1，即Substring(0,i)
            if(curLen > targetLength)
                return targetStr.Substring(0,i) + abbrevPostfix;
        }
        return targetStr;
    }

    #endregion

    #region --- String To Array ---

    //string

    public static byte StringToByte(string valueStr)
    {
        byte value;
        if(byte.TryParse(valueStr,out value))
            return value;
        else
            return 0;
    }

    public static string[] StringToStringArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        return valueStr.Split(splitSign);
    }

    public static StringArr[] StringToStringArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        StringArr[] arrArr = new StringArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new StringArr()
            {
                array = strArr1[i].Split(splitSign2)
            };

        }
        return arrArr;
    }

    //int

    public static int StringToInt(string valueStr)
    {
        int value;
        if(int.TryParse(valueStr,out value))
            return value;
        else
            return 0;
    }

    public static int[] StringToIntArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] valueArr = valueStr.Split(splitSign);
        if(valueArr == null || valueArr.Length == 0)
            return null;

        int[] intArr = new int[valueArr.Length];
        for(int i = 0; i < valueArr.Length; i++)
        {
            intArr[i] = StringToInt(valueArr[i]);
        }
        return intArr;
    }

    public static IntArr[] StringToIntArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        IntArr[] arrArr = new IntArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new IntArr()
            {
                array = StringToIntArray(strArr1[i],splitSign2)
            };

        }
        return arrArr;
    }

    //float

    public static float StringToFloat(string valueStr)
    {
        float value;
        if(float.TryParse(valueStr,out value))
            return value;
        else
            return 0;
    }

    public static float[] StringToFloatArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] valueArr = valueStr.Split(splitSign);
        if(valueArr == null || valueArr.Length == 0)
            return null;

        float[] floatArr = new float[valueArr.Length];
        for(int i = 0; i < valueArr.Length; i++)
        {
            floatArr[i] = StringToFloat(valueArr[i]);
        }
        return floatArr;
    }

    public static FloatArr[] StringToFloatArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        FloatArr[] arrArr = new FloatArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new FloatArr()
            {
                array = StringToFloatArray(strArr1[i],splitSign2)
            };

        }
        return arrArr;
    }

    //bool

    public static bool StringToBool(string valueStr)
    {
        bool value;
        if(bool.TryParse(valueStr,out value))
            return value;
        else
            return false;
    }

    public static bool[] StringToBoolArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] valueArr = valueStr.Split(splitSign);
        if(valueArr == null || valueArr.Length == 0)
            return null;

        bool[] boolArr = new bool[valueArr.Length];
        for(int i = 0; i < valueArr.Length; i++)
        {
            boolArr[i] = StringToBool(valueArr[i]);
        }
        return boolArr;
    }

    public static BoolArr[] StringToBoolArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        BoolArr[] arrArr = new BoolArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new BoolArr()
            {
                array = StringToBoolArray(strArr1[i],splitSign2)
            };

        }
        return arrArr;
    }

    //enum

    public static T StringToEnum<T>(string valueStr) where T : Enum
    {
        if(string.IsNullOrEmpty(valueStr))
            return (T)default;

        //先校验字符串是否为枚举值
        int intValue;
        if(int.TryParse(valueStr,out intValue))
        {
            if(Enum.IsDefined(typeof(T),intValue))
                return (T)Enum.ToObject(typeof(T),intValue);
        }
        //如果不是枚举值，当做枚举名处理
        try
        {
            T t = (T)Enum.Parse(typeof(T),valueStr);
            if(Enum.IsDefined(typeof(T),t))
                return t;
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
        Debug.LogError(string.Format("解析枚举错误 {0} : {1}",typeof(T),valueStr));
        return (T)default;
    }

    public static T[] StringToEnumArray<T>(string valueStr,char splitSign = '|') where T : Enum
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] valueArr = valueStr.Split(splitSign);
        if(valueArr == null || valueArr.Length == 0)
            return null;

        T[] enumArr = new T[valueArr.Length];
        for(int i = 0; i < valueArr.Length; i++)
        {
            enumArr[i] = StringToEnum<T>(valueArr[i]);
        }
        return enumArr;
    }

    ////不支持泛型枚举序列化
    //public static EnumArr<T>[] StringToEnumArray2D<T>(string valueStr,char splitSign1 = '&',char splitSign2 = '|') where T : Enum
    //{
    //    if(string.IsNullOrEmpty(valueStr))
    //        return null;
    //    string[] strArr1 = valueStr.Split(splitSign1);
    //    if(strArr1.Length == 0)
    //        return null;

    //    EnumArr<T>[] arrArr = new EnumArr<T>[strArr1.Length];
    //    for(int i = 0; i < strArr1.Length; i++)
    //    {
    //        arrArr[i] = new EnumArr<T>()
    //        {
    //            array = StringToEnumArray<T>(strArr1[i],splitSign2)
    //        };

    //    }
    //    return arrArr;
    //}

    //vector2

    public static Vector2 StringToVector2(string valueStr,char splitSign = ',')
    {
        Vector2 value = Vector2.zero;
        if(!string.IsNullOrEmpty(valueStr))
        {
            string[] stringArray = valueStr.Split(splitSign);
            if(stringArray != null && stringArray.Length >= 2)
            {
                value.x = StringToFloat(stringArray[0]);
                value.y = StringToFloat(stringArray[1]);
                return value;
            }
        }
        Debug.LogWarning("String to Vector2 error");
        return value;
    }

    public static Vector2[] StringToVector2Array(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray == null || stringArray.Length == 0)
            return null;

        Vector2[] vector2s = new Vector2[stringArray.Length];
        for(int i = 0; i < stringArray.Length; i++)
        {
            vector2s[i] = StringToVector2(stringArray[i]);
        }
        return vector2s;
    }

    public static Vector2Arr[] StringToVector2Array2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        Vector2Arr[] arrArr = new Vector2Arr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new Vector2Arr()
            {
                array = StringToVector2Array(strArr1[i],splitSign2)
            };
        }
        return arrArr;
    }

    //vector3

    public static Vector3 StringToVector3(string valueStr,char splitSign = ',')
    {
        Vector3 value = Vector3.zero;
        if(!string.IsNullOrEmpty(valueStr))
        {
            string[] stringArray = valueStr.Split(splitSign);
            if(stringArray.Length >= 3)
            {
                value.x = StringToFloat(stringArray[0]);
                value.y = StringToFloat(stringArray[1]);
                value.z = StringToFloat(stringArray[2]);
                return value;
            }
        }
        Debug.LogWarning("String to Vector3 error");
        return value;
    }

    public static Vector3[] StringToVector3Array(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray == null || stringArray.Length == 0)
            return null;

        Vector3[] vector3s = new Vector3[stringArray.Length];
        for(int i = 0; i < stringArray.Length; i++)
        {
            vector3s[i] = StringToVector3(stringArray[i]);
        }
        return vector3s;
    }

    public static Vector3Arr[] StringToVector3Array2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        Vector3Arr[] arrArr = new Vector3Arr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new Vector3Arr()
            {
                array = StringToVector3Array(strArr1[i],splitSign2)
            };
        }
        return arrArr;
    }

    //vector2Int

    public static Vector2Int StringToVector2Int(string valueStr,char splitSign = ',')
    {
        Vector2Int value = Vector2Int.zero;
        if(!string.IsNullOrEmpty(valueStr))
        {
            string[] stringArray = valueStr.Split(splitSign);
            if(stringArray != null && stringArray.Length >= 2)
            {
                value.x = StringToInt(stringArray[0]);
                value.y = StringToInt(stringArray[1]);
                return value;
            }
        }
        Debug.LogWarning("String to Vector2Int error");
        return value;
    }

    public static Vector2Int[] StringToVector2IntArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray == null || stringArray.Length == 0)
            return null;

        Vector2Int[] vector2Ints = new Vector2Int[stringArray.Length];
        for(int i = 0; i < stringArray.Length; i++)
        {
            vector2Ints[i] = StringToVector2Int(stringArray[i]);
        }
        return vector2Ints;
    }

    public static Vector2IntArr[] StringToVector2IntArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        Vector2IntArr[] arrArr = new Vector2IntArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new Vector2IntArr()
            {
                array = StringToVector2IntArray(strArr1[i],splitSign2)
            };
        }
        return arrArr;
    }

    //vector3Int

    public static Vector3Int StringToVector3Int(string valueStr,char splitSign = ',')
    {
        Vector3Int value = Vector3Int.zero;
        if(!string.IsNullOrEmpty(valueStr))
        {
            string[] stringArray = valueStr.Split(splitSign);
            if(stringArray.Length >= 3)
            {
                value.x = StringToInt(stringArray[0]);
                value.y = StringToInt(stringArray[1]);
                value.z = StringToInt(stringArray[2]);
                return value;
            }
        }
        Debug.LogWarning("String to Vector3 error");
        return value;
    }

    public static Vector3Int[] StringToVector3IntArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray == null || stringArray.Length == 0)
            return null;

        Vector3Int[] vector3Ints = new Vector3Int[stringArray.Length];
        for(int i = 0; i < stringArray.Length; i++)
        {
            vector3Ints[i] = StringToVector3Int(stringArray[i]);
        }
        return vector3Ints;
    }

    public static Vector3IntArr[] StringToVector3IntArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        Vector3IntArr[] arrArr = new Vector3IntArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new Vector3IntArr()
            {
                array = StringToVector3IntArray(strArr1[i],splitSign2)
            };
        }
        return arrArr;
    }

    //color

    public static Color StringToColor(string valueStr,char splitSign = ',')
    {
        if(string.IsNullOrEmpty(valueStr))
            return Color.white;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray.Length < 3)
            return Color.white;

        Color color = new Color()
        {
            r = StringToFloat(stringArray[0]),
            g = StringToFloat(stringArray[1]),
            b = StringToFloat(stringArray[2]),
            a = stringArray.Length < 4 ? 1 : StringToFloat(stringArray[3])
        };
        return color;
    }
    public static Color32 StringToColor32(string valueStr,char splitSign = ',')
    {
        if(string.IsNullOrEmpty(valueStr))
            return Color.white;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray.Length < 3)
            return Color.white;

        Color32 color = new Color32()
        {
            r = StringToByte(stringArray[0]),
            g = StringToByte(stringArray[1]),
            b = StringToByte(stringArray[2]),
            a = stringArray.Length < 4 ? (byte)1 : StringToByte(stringArray[3])
        };
        return color;
    }

    public static Color[] StringToColorArray(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray == null || stringArray.Length == 0)
            return null;

        Color[] colors = new Color[stringArray.Length];
        for(int i = 0; i < stringArray.Length; i++)
        {
            colors[i] = StringToColor(stringArray[i]);
        }
        return colors;
    }

    public static Color32[] StringToColor32Array(string valueStr,char splitSign = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;

        string[] stringArray = valueStr.Split(splitSign);
        if(stringArray == null || stringArray.Length == 0)
            return null;

        Color32[] colors = new Color32[stringArray.Length];
        for(int i = 0; i < stringArray.Length; i++)
        {
            colors[i] = StringToColor32(stringArray[i]);
        }
        return colors;
    }

    public static ColorArr[] StringToColorArray2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        ColorArr[] arrArr = new ColorArr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new ColorArr()
            {
                array = StringToColorArray(strArr1[i],splitSign2)
            };
        }
        return arrArr;
    }
    public static Color32Arr[] StringToColor32Array2D(string valueStr,char splitSign1 = '&',char splitSign2 = '|')
    {
        if(string.IsNullOrEmpty(valueStr))
            return null;
        string[] strArr1 = valueStr.Split(splitSign1);
        if(strArr1.Length == 0)
            return null;

        Color32Arr[] arrArr = new Color32Arr[strArr1.Length];
        for(int i = 0; i < strArr1.Length; i++)
        {
            arrArr[i] = new Color32Arr()
            {
                array = StringToColor32Array(strArr1[i],splitSign2)
            };
        }
        return arrArr;
    }

    #endregion

    #region MyRegion

    public static string GetRandomString(int length)
    {
        StringBuilder builder = new StringBuilder();
        string abc = "abcdefghijklmnopqrstuvwxyzo0123456789QWERTYUIOPASDFGHJKLZXCCVBMN";
        for(int i = 0; i < length; i++)
        {
            builder.Append(abc[UnityEngine.Random.Range(0,abc.Length - 1)]);
        }
        return builder.ToString();
    }

    public static string Join<T>(T[] arr,string join = ",")
    {
        if(arr == null || arr.Length == 0)
            return null;

        StringBuilder builder = new StringBuilder();
        for(int i = 0; i < arr.Length; i++)
        {
            builder.Append(arr[i]);
            if(i < arr.Length - 1)
                builder.Append(join);
        }
        return builder.ToString();
    }

    /// <summary>
    /// 中文逗号转英文逗号
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToDBC(string input)
    {
        char[] c = input.ToCharArray();
        for(int i = 0; i < c.Length; i++)
        {
            if(c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }
            if(c[i] > 65280 && c[i] < 65375)
                c[i] = (char)(c[i] - 65248);
        }
        return new string(c);
    }

    /// <summary>
    /// 字符转 ascii 码 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public static int Asc(string character)
    {
        if(character.Length == 1)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
            return (intAsciiCode);
        }
        Debug.LogError("Character is not valid.");
        return -1;
    }

    /// <summary>
    /// ascii码转字符
    /// </summary>
    /// <param name="asciiCode"></param>
    /// <returns></returns>
    public static string Chr(int asciiCode)
    {
        if(asciiCode >= 0 && asciiCode <= 255)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArray = new byte[] { (byte)asciiCode };
            string strCharacter = asciiEncoding.GetString(byteArray);
            return (strCharacter);
        }
        Debug.LogError("ASCII Code is not valid.");
        return string.Empty;
    }

    /// <summary>
    /// 过滤掉表情符号
    /// </summary>
    /// <returns>The emoji.</returns>
    /// <param name="str">String.</param>
    public static string FilterEmoji(string str)
    {
        List<string> patten = new List<string>() { @"\p{Cs}",@"\p{Co}",@"\p{Cn}",@"[\u2702-\u27B0]" };
        for(int i = 0; i < patten.Count; i++)
        {
            str = Regex.Replace(str,patten[i],"");//屏蔽emoji   
        }
        return str;
    }

    /// <summary>
    /// 过滤掉表情符号
    /// </summary>
    /// <returns>The emoji.</returns>
    /// <param name="str">String.</param>
    public static bool IsFilterEmoji(string str)
    {
        bool bEmoji = false;
        List<string> patten = new List<string>() { @"\p{Cs}",@"\p{Co}",@"\p{Cn}",@"[\u2702-\u27B0]" };
        for(int i = 0; i < patten.Count; i++)
        {
            bEmoji = Regex.IsMatch(str,patten[i]);
            if(bEmoji)
            {
                break;
            }
        }
        return bEmoji;
    }

    #endregion

    #region StringObjectDictionaryExtensions

    /// <summary>
    /// 针对字典中包含以下键值进行结构：mctid0=xxx;mccount0=1,mctid1=kn2,mccount=2。将其前缀去掉，数字后缀变为键，如{后缀,(去掉前后缀的键,值)}，注意后缀可能是空字符串即没有后缀
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="prefix">前缀，可以是空引用或空字符串，都表示没有前缀。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IGrouping<string,(string, object)>> GetValuesWithoutPrefix(this IReadOnlyDictionary<string,object> dic,string prefix = null)
    {
        //prefix ??= string.Empty;
        prefix = prefix ?? string.Empty;

        var coll = from tmp in dic.Where(c => c.Key.StartsWith(prefix)) //仅针对指定前缀的键值
                   let p3 = tmp.Key.Get3Segment(prefix)
                   group (p3.Item2, tmp.Value) by p3.Item3;
        return coll;
    }

    /// <summary>
    /// 分解字符串为三段，前缀，词根，数字后缀(字符串形式)。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="prefix">前缀，可以是空引用或空字符串，都表示没有前缀。</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (string, string, string) Get3Segment(this string str,string prefix = null)
    {
        //prefix ??= string.Empty;
        prefix = prefix ?? string.Empty;

        //最后十进制数字尾串的长度
        int suffixLen = Enumerable.Reverse(str).TakeWhile(c => char.IsDigit(c)).Count();
        //获取十进制数字后缀
        //string suufix = str[^suffixLen..];     //^suffixLen：倒序下标；suffixLen..：从指定位置开始直到末尾
        string suufix = str.Substring(str.Length - suffixLen);

        //return (prefix, str[prefix.Length..^suufix.Length], suufix);
        string middle = str.Substring(prefix.Length,str.Length - prefix.Length - suufix.Length);
        return (prefix, middle, suufix);
    }

    #endregion

}
