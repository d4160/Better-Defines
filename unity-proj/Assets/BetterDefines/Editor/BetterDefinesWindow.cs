using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BetterDefines.Editor
{
    public class BetterDefinesWindow : EditorWindow
    {
        private SerializedObject settingsSerializedObject;
        private ReorderableList list;

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
            list = new ReorderableList(settingsSerializedObject, settingsSerializedObject.FindProperty("Defines"), true, true, true, true);
        }

        void OnGUI()
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