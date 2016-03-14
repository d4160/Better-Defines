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
                true, false, true);
            list.drawHeaderCallback += rect =>
            {
                EditorGUI.LabelField(rect, "Custom Scripting Define Symbols");
            };
            list.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.y += 2;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                var defineProp = element.FindPropertyRelative("Define");

                EditorGUI.BeginChangeCheck();
                EditorGUI.SelectableLabel(new Rect(rect.x, rect.y, rect.width*0.25f, EditorGUIUtility.singleLineHeight),
                    defineProp.stringValue);
                if (EditorGUI.EndChangeCheck())
                {
                    defineProp.stringValue = defineProp.stringValue;
                }

                DrawPlatformToggles(rect, defineProp.stringValue);
                DrawActionButton(rect);
            };
            return list;
        }

        private static void DrawPlatformToggles(Rect rect, string define)
        {
            var settings = BetterDefinesSettings.Instance;
            var platformsWidth = rect.width*0.7f;
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
            var platformsXStartPos = rect.x + rect.width * 0.23f;
            return new Rect(platformsXStartPos + index * singleToggleWidth, rect.y, singleToggleWidth, EditorGUIUtility.singleLineHeight);
        }

        private static void DrawActionButton(Rect rect)
        {
            var xPos = rect.x + rect.width * 0.95f;
            var width = rect.width*0.05f;
            GUI.color = Color.green;
            if (GUI.Button(new Rect(xPos, rect.y, width, EditorGUIUtility.singleLineHeight), "+"))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("Remove From All Platfroms"), false, ActionClickHandler, "DELETE_ALL");
                menu.ShowAsContext();
            }
            GUI.color = Color.white;
        }

        private static void ActionClickHandler(object target)
        {
            var data = (string) target;
            if (data == "DELETE_ALL")
            {

            }
        }
    }
}