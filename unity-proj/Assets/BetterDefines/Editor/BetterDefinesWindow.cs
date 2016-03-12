using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BetterDefines.Editor
{
    public class BetterDefinesWindow : EditorWindow
    {
        private bool drawMainDefines = true;
        private ReorderableList list;
        private SerializedObject settingsSerializedObject;

        [MenuItem("Window/Better Defines")]
        private static void Init()
        {
            var window = (BetterDefinesWindow) GetWindow(typeof (BetterDefinesWindow));
#if UNITY_5_3
            window.titleContent = new GUIContent("Defines");
#else
            window.title = "Defines";
#endif

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
            if (drawMainDefines)
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
            if (GUILayout.Toggle(drawMainDefines, "Custom Defines", "toolbarbutton"))
            {
                drawMainDefines = true;
            }
            if (GUILayout.Toggle(!drawMainDefines, "Preferences", "toolbarbutton"))
            {
                drawMainDefines = false;
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