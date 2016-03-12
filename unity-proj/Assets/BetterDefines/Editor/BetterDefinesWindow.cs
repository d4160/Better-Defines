using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BetterDefines.Editor
{
    public class BetterDefinesWindow : EditorWindow
    {
        private bool _drawMainDefines = true;
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
            list = DefinesReorderableList.Create(settingsSerializedObject);
        }

        private void OnGUI()
        {
            DrawTopSettingsTabs();
            if (settingsSerializedObject == null)
            {
                LoadSettings();
            }
            settingsSerializedObject.Update();
            if (_drawMainDefines)
            {
                list.DoLayoutList();
            }
            else
            {
                DrawPreferences();
            }
            settingsSerializedObject.ApplyModifiedProperties();
        }

        private void DrawTopSettingsTabs()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Toggle(_drawMainDefines, "Custom Defines", "toolbarbutton"))
            {
                _drawMainDefines = true;
            }
            if (GUILayout.Toggle(!_drawMainDefines, "Preferences", "toolbarbutton"))
            {
                _drawMainDefines = false;
            }
            GUILayout.EndHorizontal();
        }

        private void DrawPreferences()
        {
            EditorGUILayout.HelpBox("Please disable platforms that you are not using in your project. " +
                                    "For disabled platforms toggles will not be displayed in defines tab", MessageType.Info);
            foreach (var platform in EditorUtils.AllBuildPlatforms)
            {
                var setting = BetterDefinesSettings.Instance.GetGlobalPlatformState(platform.Id);

                if (setting.PlatformId == EditorUtils.STANDALONE_PLATFORM_ID) { GUI.enabled = false; }
                setting.IsEnabled = GUILayout.Toggle(setting.IsEnabled, new GUIContent(" " + platform.Name, platform.Icon));
                if (setting.PlatformId == EditorUtils.STANDALONE_PLATFORM_ID) { GUI.enabled = true; }
            }
        }
    }
}