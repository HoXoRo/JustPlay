using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Editor
{
    public class XrEditorWindow : EditorWindow
    {

        [MenuItem("[Xr]/Profile %`")]
        private static void ShowWindow()
        {
            var window = GetWindow<XrEditorWindow>(typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow"));
            window.Focus();
            window.titleContent = new GUIContent("[Xr]");
        }

        private void OnEnable()
        {
            
        }

        private Vector2 scrollPosition = Vector2.zero;
        private void OnGUI()
        {
            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
            {
                scrollPosition = scrollViewScope.scrollPosition;
                using (new EditorGUILayout.VerticalScope())
                {
                    var guiContent = EditorGUIKit.GetGUIContent("AssetBundle模块", "d_ToolsToggle");
                    EditorGUIKit.InitExpendBox(guiContent, true, expend => {
                        if (GUILayout.Button("一键打包", GUILayout.Height(30)))
                        {
                        }
                    });
                }
            }
        }
    }
}