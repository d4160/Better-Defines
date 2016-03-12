using System.Linq;
using BetterDefines.Editor.Entity;
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
                var filteredPlatfroms = EditorUtils.AllBuildPlatforms.Where(x => BetterDefinesSettings.Instance.GetGlobalPlatformState(x.Id).IsEnabled).ToList();
                var oneWidth = platformsWidth / filteredPlatfroms.Count;
                for (int i = 0; i < filteredPlatfroms.Count; i++)
                {
                    if (GUI.Toggle(new Rect(rect.width * 0.28f + i * oneWidth, rect.y, oneWidth, EditorGUIUtility.singleLineHeight * 0.9f), i % 2 == 0, new GUIContent(EditorUtils.AllBuildPlatforms[i].Icon, "Standalone"), EditorStyles.toolbarButton))
                    {
                    }
                }
                //if (GUI.Toggle(new Rect(rect.x + 300, rect.y, 100, EditorGUIUtility.singleLineHeight * 0.9f), true, new GUIContent(EditorUtils.StandaloneIcon, "Standalone"), EditorStyles.toolbarButton))
                //{

                //}
            };
            return list;
        }
    }
}