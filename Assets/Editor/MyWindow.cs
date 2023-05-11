using System;
using UnityEditor;
using UnityEditor.iOS;
using UnityEngine;

public class MyWindow : EditorWindow
{
    [MenuItem("德玛/Window/NormalWindow")]
    static void NormalWindow()
    {
        var windowType = 1;
        MyWindow myWindow = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow), true, "德玛标题", true);
        myWindow.Show();
    }

    public void Awake()
    {
        var texture = Resources.Load("1") as Texture;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("选中");
        EditorGUILayout.LabelField(EditorWindow.focusedWindow.ToString());
        EditorGUILayout.LabelField("划入");
        EditorGUILayout.LabelField(EditorWindow.mouseOverWindow.ToString());
    }
}

