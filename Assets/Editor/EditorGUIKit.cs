using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

namespace Editor
{
    public class EditorGUIKit
    {
        public static GUIContent GetGUIContent(string text, string icon = null, string toolTip = null)
        {
            var guiContent = string.IsNullOrEmpty(icon)
                ? new GUIContent(EditorGUIUtility.IconContent(icon))
                : new GUIContent();

            guiContent.text = text;
            guiContent.tooltip = toolTip;

            return guiContent;
        }
        
        public static void InitExpendBox(GUIContent titleContent, bool isShow, UnityAction<bool> action,
            AnimBool animBool = null)
        {
            var titleStyle = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(5, 5, 5, 5)
            };
            using (new EditorGUILayout.VerticalScope(titleStyle))
            {
                InitExpendTitle(titleContent, isShow, isExpand =>
                {
                    var style = new GUIStyle(GUI.skin.textArea)
                    {
                        padding = new RectOffset(7, 7, 7, 7),
                        margin = new RectOffset(0, 0, 0, 0)
                    };
                    if (!isExpand)
                    {
                        if (animBool == null)
                        {
                            action?.Invoke(isExpand);
                        }
                        else if (animBool.isAnimating)
                        {
                            using (new EditorGUILayout.VerticalScope(style))
                            {
                                action?.Invoke(isExpand);
                            }
                        }
                    }
                    else
                    {
                        using (new EditorGUILayout.VerticalScope(style))
                        {
                            action?.Invoke(isExpand);
                        }
                    }
                }, animBool);
            }
        }

        public static void InitExpendTitle(GUIContent smallTabTitle, bool isShow, UnityAction<bool> action,
            AnimBool animBool = null)
        {
            using (new EditorGUILayout.HorizontalScope("ScriptText", GUILayout.Height(28)))
            {
                var style = new GUIStyle(EditorStyles.foldout)
                {
                    fixedHeight = 22,
                    margin = new RectOffset(2, 0, 0, 0)
                };

                isShow = EditorGUILayout.Foldout(isShow, smallTabTitle, true, style);
            }

            if (animBool != null)
            {
                animBool.target = isShow;
                using (new EditorGUILayout.FadeGroupScope(animBool.faded))
                {
                    action?.Invoke(isShow);
                }
            }
            else
            {
                action?.Invoke(isShow);
            }
        }
    }
}