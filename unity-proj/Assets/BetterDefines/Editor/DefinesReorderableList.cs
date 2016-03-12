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
                var defineProp = element.FindPropertyRelative("Define");

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width*0.25f, EditorGUIUtility.singleLineHeight),
                    defineProp, GUIContent.none);

                DrawPlatformToggles(rect, defineProp.stringValue);
                //if (GUI.Toggle(new Rect(rect.x + 300, rect.y, 100, EditorGUIUtility.singleLineHeight * 0.9f), true, new GUIContent(EditorUtils.StandaloneIcon, "Standalone"), EditorStyles.toolbarButton))
                //{

                //}
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