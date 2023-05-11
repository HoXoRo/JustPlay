using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssestBundleTool
{
    [MenuItem("AssestBundleTool/CreatAssestBundle for Android")]
    static void CreatAssestBundle()
    {
        string path = "Assets/StreamingAssets";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
        UnityEngine.Debug.Log("Android Finish!");
    }
    
    [MenuItem("AssestBundleTool/CreatAssestBundle for IOS")]
    static void CreatAssestBundleForIOS()
    {
        string path = "Assets/IOS";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iOS);
        UnityEngine.Debug.Log("IOS Finish!");
    }

    [MenuItem("AssestBundleTool/CreatAssestBundle for Win")]
    static void CreatAssestBundleForWin()
    {
        string path = "AB";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        UnityEngine.Debug.Log("Windows Finish!");
    }
    
    
    [@MenuItem("Tools/BuildAB")]
    public static void BuildAB()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.StrictMode, BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
    }
}
