using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    private string path = "AB/jojo.png";
    private AssetBundle asset;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            asset = AssetBundle.LoadFromFile(path);
            asset.LoadAsset("jojo");
            // asset.LoadAsset("Giorno");
            Debug.Log("Load AssetBundle");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            asset.Unload(true);
            Debug.Log("Unlload AssetBundle");
        }
    }
    
    [ContextMenu("FunctionName")]
    public void FunctionName()
    {
        //ToDo
    }


    [ContextMenuItem("Handle", "HandleHealth")]
    public float health;

    private void HandleHealth()
    {
        health = 120;
    }

    [Header("名字")] public string Name;
    [Tooltip("年龄")] public int Age;
    [Space(2)] [TextArea] public string Description;
    [Range(0f, 100f)] public float Score;
    
    
    [SerializeField] 
    private int privateInt;

    [ContextMenu("OutputInfo")]
    void OutputInfo()
    {
        Debug.Log("outputinfo");
    }


    [Header("BaseInfo")] [Multiline(5)] public string myString;

    [Range(-2, 2)] public int count;
}
