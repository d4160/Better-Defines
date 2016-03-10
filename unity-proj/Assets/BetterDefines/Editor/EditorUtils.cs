using System;
using System.IO;
using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEngine;

namespace BetterDefines.Editor
{
    [InitializeOnLoad]
    public static class EditorUtils
    {
        public static Texture2D StandaloneIcon;

        static EditorUtils()
        {
            StandaloneIcon = EditorGUIUtility.IconContent("BuildSettings.Standalone.Small").image as Texture2D;
        }

        // TODO Remove
        [MenuItem("Better Defines/Create Settings")]
        public static void CreateSettings()
        {
            CreateAsset<BetterDefinesSettings>(BetterDefinesSettings.SETTINGS_RESOURCE_NAME);
        }

        /// <summary>
        ///     Creates .asset file of the specified <see cref="UnityEngine.ScriptableObject" />
        /// </summary>
        public static void CreateAsset<T>(string baseName, string forcedPath = "") where T : ScriptableObject
        {
            if (baseName.Contains("/"))
                throw new ArgumentException("Base name should not contain slashes");

            var asset = ScriptableObject.CreateInstance<T>();

            string path;
            if (!string.IsNullOrEmpty(forcedPath))
            {
                path = forcedPath;
                Directory.CreateDirectory(forcedPath);
            }
            else
            {
                path = AssetDatabase.GetAssetPath(Selection.activeObject);

                if (string.IsNullOrEmpty(path))
                {
                    path = "Assets";
                }
                else if (Path.GetExtension(path) != string.Empty)
                {
                    path = path.Replace(Path.GetFileName(path), string.Empty);
                }
            }

            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + baseName + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}