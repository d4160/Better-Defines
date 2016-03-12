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
            var listSerializedProperty = settingsSerializedObject.FindProperty("Defines");
            var list = new ReorderableList(settingsSerializedObject, listSerializedProperty,
                true,
                false, true, true)
            {
                headerHeight = 0f
            };
            list.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.y += 2;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                var defineProp = element.FindPropertyRelative("Define");

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width*0.25f, EditorGUIUtility.singleLineHeight),
                    defineProp, GUIContent.none);

                DrawPlatformToggles(rect, defineProp.stringValue);
                //if (GUI.Toggle(new Rect(rect.x + 300, rect.y, 100, EditorGUIUtility.singleLineHeight * 0.9f), true, new GUIContent(EditorUtils.StandaloneIcon, "Standalone"), EditorStyles.toolbarButton))
                //{

                //}
            };
            list.onAddCallback += reorderableList =>
            {
                settingsSerializedObject.Update();
                var index = listSerializedProperty.arraySize;
                list.serializedProperty.arraySize++;
                list.index = index;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                var define = element.FindPropertyRelative("Define");

                if (list.serializedProperty.arraySize == 1)
                {
                    define.stringValue = "EXAMPLE_DEFINE";
                }
                else
                {
                    var previousDefine = list.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("Define").stringValue;
                    define.stringValue = previousDefine + "_NEW"; // we need a unique name
                }

                // disable all by default
                var defineSettings = element.FindPropertyRelative("StatesForPlatforms");
                for (int i = 0; i < defineSettings.arraySize; i++)
                {
                    defineSettings.GetArrayElementAtIndex(i).FindPropertyRelative("IsEnabled").boolValue = true;
                }

                settingsSerializedObject.ApplyModifiedProperties();
            };
            return list;
        }

        private static void DrawPlatformToggles(Rect rect, string define)
        {
            var settings = BetterDefinesSettings.Instance;
            var platformsWidth = rect.width*0.75f;
            var filteredPlatforms = EditorUtils.AllBuildPlatforms.Where(x => settings.GetGlobalPlatformState(x.Id).IsEnabled).ToList();
            var singleToggleWidth = platformsWidth/filteredPlatforms.Count;

            for (var i = 0; i < filteredPlatforms.Count; i++)
            {
                var isEnabled = settings.GetDefineState(define, filteredPlatforms[i].Id);
                var result = GUI.Toggle(GetPlatformToggleRect(rect, singleToggleWidth, i), isEnabled,
                    filteredPlatforms[i].ToGUIContent(), EditorStyles.toolbarButton);
                settings.SetDefineState(define, filteredPlatforms[i].Id, result);
            }
        }

        private static Rect GetPlatformToggleRect(Rect rect, float singleToggleWidth, int index)
        {
            var platformsXStartPos = rect.x + rect.width*0.25f;
            return new Rect(platformsXStartPos + index*singleToggleWidth, rect.y, singleToggleWidth, EditorGUIUtility.singleLineHeight);
        }
    }
}