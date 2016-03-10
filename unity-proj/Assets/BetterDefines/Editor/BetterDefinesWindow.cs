using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BetterDefines.Editor
{
    public class BetterDefinesWindow : EditorWindow
    {
        private ReorderableList list;
        private SerializedObject settingsSerializedObject;

        [MenuItem("Window/Better Defines")]
        private static void Init()
        {
            var window = (BetterDefinesWindow) GetWindow(typeof (BetterDefinesWindow));
            window.titleContent = new GUIContent("Defines");
            window.Show();
        }

        private void LoadSettings()
        {
            settingsSerializedObject = new SerializedObject(BetterDefinesSettings.Instance);
            InitializeList();
        }

        private void InitializeList()
        {
            list = new ReorderableList(settingsSerializedObject, settingsSerializedObject.FindProperty("Defines"), true,
                true, true, true);
            list.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.y += 2;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width * 0.35f, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Define"), GUIContent.none);
            };
        }

        private void OnGUI()
        {
            if (settingsSerializedObject == null)
            {
                LoadSettings();
            }
            settingsSerializedObject.Update();
            list.DoLayoutList();
            settingsSerializedObject.ApplyModifiedProperties();
        }
    }
}