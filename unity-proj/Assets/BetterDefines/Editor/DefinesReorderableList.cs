using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BetterDefines.Editor
{
    public static class DefinesReorderableList
    {
        public static ReorderableList Create(SerializedObject settingsSerializedObject)
        {
            var list = new ReorderableList(settingsSerializedObject, settingsSerializedObject.FindProperty("Defines"),
                true,
                false, true, true)
            {
                headerHeight = 0f
            };
            list.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.y += 2;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width * 0.25f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Define"), GUIContent.none);
                var platformsWidth = rect.width * 0.75f;
                var oneWidth = platformsWidth / 15;
                for (int i = 0; i < 15; i++)
                {
                    if (GUI.Toggle(new Rect(rect.width * 0.29f + i * oneWidth, rect.y, oneWidth, EditorGUIUtility.singleLineHeight * 0.9f), i % 2 == 0, new GUIContent(EditorUtils.StandaloneIcon, "Standalone"), EditorStyles.toolbarButton))
                    {
                    }
                }
                //if (GUI.Toggle(new Rect(rect.x + 300, rect.y, 100, EditorGUIUtility.singleLineHeight * 0.9f), true, new GUIContent(EditorUtils.StandaloneIcon, "Standalone"), EditorStyles.toolbarButton))
                //{

                //}
            };
            //list.drawElementBackgroundCallback = (rect, index, active, focused) =>
            //{
            //    Texture2D tex = new Texture2D(1, 1);
            //    tex.SetPixel(0, 0, new Color(0.33f, 0.66f, 1f, 0.66f));
            //    tex.Apply();
            //    if (active)
            //    {
            //        rect.height = 40f;
            //        GUI.DrawTexture(rect, tex as Texture);
            //    }
            //};
            //list.elementHeightCallback += index => 40f;
            return list;
        }
    }
}