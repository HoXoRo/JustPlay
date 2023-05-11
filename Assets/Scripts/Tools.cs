using System;
using UnityEditor;
using UnityEngine;


[AddComponentMenu("Laern/test")]
public class Tools : MonoBehaviour
{
    [MenuItem("[MenuItem]/测试1")]
    private static void MenuItenFunc()
    {
        Debug.Log("测试1");
    }

    // [MenuItem("[MenuItem]/测试2 GameObject", false)]
    // private static bool MenuItenFunc1()
    // {
    //     Debug.Log("测试2");
    //
    //     Object selectedObj = Selection.activeObject;
    //     if (selectedObj != null && selectedObj.GetType() == typeof(GameObject))
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }

    [MenuItem("CONTEXT/Rigidbody/在Rigidbody右键")]
    private static void CONTEXT_Rigidbody_right_btn()
    {
        Debug.Log("在Rigidbody上右键");
    }

    [MenuItem("GameObject/UI/在GameObject目录里右键")]
    private static void GameObject_right_btn()
    {
        Debug.Log("在GameObject目录里右键");
    }

    [ContextMenu("ContextMenu")]
    public void ContextMenuFunc()
    {
        Debug.Log("ContextMenu1");
    }


    [ContextMenuItem("add testName", "ContextMenuFunc2")]
    public string testName = "";

    private void ContextMenuFunc2()
    {
        testName = "testName";
    }


    [ContextMenu(("FunctionName"))]
    public void FunctionName()
    {
        
    }


}
