using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class BuildExcelWindow : EditorWindow
{
    [MenuItem("MyTools/Excel Window",priority = 100)]
    public static void ShowReadExcelWindow()
    {
        BuildExcelWindow window = GetWindow<BuildExcelWindow>(true);
        window.Show();
        window.minSize = new Vector2(475,475);
    }

    //Excel读取路径，绝对路径，放在Assets同级路径
    private static string excelReadAbsolutePath;

    //自动生成C#类文件路径，绝对路径
    private static string scriptSaveAbsolutePath;
    private static string scriptSaveRelativePath;
    //自动生成Asset文件路径，相对路径
    private static string assetSaveRelativePath;

    private List<string> fileNameList = new List<string>();
    private List<string> filePathList = new List<string>();

    private void Awake()
    {
        titleContent.text = "Excel配置表读取";

        excelReadAbsolutePath = Application.dataPath.Replace("Assets","Excel");
        scriptSaveAbsolutePath = Application.dataPath + CheckEditorPath("/Script/Excel/AutoCreateCSCode");
        scriptSaveRelativePath = CheckEditorPath("Assets/Script/Excel/AutoCreateCSCode");
        assetSaveRelativePath = CheckEditorPath("Assets/AssetData/Excel/AutoCreateAsset");
    }

    private void OnEnable()
    {
        RefreshExcelFile();
    }

    private void OnDisable()
    {
        fileNameList.Clear();
        filePathList.Clear();
    }

    private Vector2 scrollPosition = Vector2.zero;
    private void OnGUI()
    {
        GUILayout.Space(10);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition,GUILayout.Width(position.width),GUILayout.Height(position.height));

        //展示路径
        GUILayout.BeginHorizontal(GUILayout.Height(20));
        if(GUILayout.Button("Excel读取路径",GUILayout.Width(100)))
        {
            EditorUtility.OpenWithDefaultApp(excelReadAbsolutePath);
            Debug.Log(excelReadAbsolutePath);
        }
        if(GUILayout.Button("Script保存路径",GUILayout.Width(100)))
        {
            SelectObject(scriptSaveRelativePath);
        }
        if(GUILayout.Button("Asset保存路径",GUILayout.Width(100)))
        {
            SelectObject(assetSaveRelativePath);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        //Excel列表

        GUILayout.Label("Excel列表：");
        for(int i = 0; i < fileNameList.Count; i++)
        {
            GUILayout.BeginHorizontal("Box",GUILayout.Height(40));

            GUILayout.Label($"{i}:","Titlebar Foldout",GUILayout.Width(30),GUILayout.Height(35));
            GUILayout.Box(fileNameList[i],"MeTransitionBlock",GUILayout.MinWidth(200),GUILayout.Height(35));
            GUILayout.Space(10);

            //生成CS代码
            if(GUILayout.Button("Create Script",GUILayout.Width(100),GUILayout.Height(30)))
            {
                ExcelDataReader.ReadOneExcelToCode(filePathList[i],scriptSaveAbsolutePath);
            }
            //生成Asset文件
            if(GUILayout.Button("Create Asset",GUILayout.Width(100),GUILayout.Height(30)))
            {
                ExcelDataReader.CreateOneExcelAsset(filePathList[i],assetSaveRelativePath);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
        GUILayout.Space(10);

        //一键处理所有Excel

        GUILayout.Label("一键操作：");
        GUILayout.BeginHorizontal("Box",GUILayout.Height(40));

        GUILayout.Label("all","Titlebar Foldout",GUILayout.Width(30),GUILayout.Height(35));
        GUILayout.Box("All Excel","MeTransitionBlock",GUILayout.MinWidth(200),GUILayout.Height(35));
        GUILayout.Space(10);

        if(GUILayout.Button("Create Script",GUILayout.Width(100),GUILayout.Height(30)))
        {
            ExcelDataReader.ReadAllExcelToCode(excelReadAbsolutePath,scriptSaveAbsolutePath);
        }
        if(GUILayout.Button("Create Asset",GUILayout.Width(100),GUILayout.Height(30)))
        {
            ExcelDataReader.CreateAllExcelAsset(excelReadAbsolutePath,assetSaveRelativePath);
        }
        GUILayout.EndHorizontal();

        //
        GUILayout.Space(20);
        //
        GUILayout.EndScrollView();
    }

    //读取指定路径下的Excel文件名
    private void RefreshExcelFile()
    {
        fileNameList.Clear();
        filePathList.Clear();

        if(!Directory.Exists(excelReadAbsolutePath))
        {
            Debug.LogError("无效路径(尝试重新开启窗口)：" + excelReadAbsolutePath);
            return;
        }
        string[] excelFileFullPaths = Directory.GetFiles(excelReadAbsolutePath,"*.xlsx");

        if(excelFileFullPaths == null || excelFileFullPaths.Length == 0)
        {
            Debug.LogError(excelReadAbsolutePath + "路径下没有找到Excel文件");
            return;
        }

        filePathList.AddRange(excelFileFullPaths);
        for(int i = 0; i < filePathList.Count; i++)
        {
            fileNameList.Add(Path.GetFileName(filePathList[i]));
        }
        Debug.Log("找到Excel文件：" + fileNameList.Count + "个");
    }

    private void SelectObject(string targetPath)
    {
        Object targetObj = AssetDatabase.LoadAssetAtPath<Object>(targetPath);
        EditorGUIUtility.PingObject(targetObj);
        Selection.activeObject = targetObj;
        Debug.Log(targetPath);
    }

    private static string CheckEditorPath(string path)
    {
#if UNITY_EDITOR_WIN
        return path.Replace("/","\\");
#elif UNITY_EDITOR_OSX
        return path.Replace("\\","/");
#else
        return path;
#endif
    }
}