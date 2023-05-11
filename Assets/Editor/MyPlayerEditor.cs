using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(MyPlayer))]
public class MyPlayerEditor : UnityEditor.Editor
{
    SerializedProperty attack;
    void OnEnable()
    {
        attack = serializedObject.FindProperty("attack");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.IntSlider(attack, 0, 100, new GUIContent("攻击力"));
        ProgressBar(attack.intValue / 100, "攻击力");
        serializedObject.ApplyModifiedProperties();
    }
    private void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}