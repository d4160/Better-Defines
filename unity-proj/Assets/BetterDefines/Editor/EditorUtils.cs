using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEngine;

namespace BetterDefines.Editor
{
    [InitializeOnLoad]
    public static class EditorUtils
    {
        public const string WEB_PLAYER_PLATFORM_ID = "Web";
        public const string STANDALONE_PLATFORM_ID = "Standalone";
        public const string IOS_PLATFORM_ID = "iPhone";
        public const string ANDROID_ID = "Android";
        public const string BLACKBERRY_PLATFORM_ID = "BlackBerry";
        public const string TIZEN_PLATFORM_ID = "Tizen";
        public const string XBOX360_PLATFORM_ID = "XBox360";
        public const string XBOX_ONE_PLATFORM_ID = "XboxOne";
        public const string PS3_PLATFORM_ID = "PS3";
        public const string PS_VITA_PLATFORM_ID = "PSP2";
        public const string PS4_PLATFORM_ID = "PS4";

        public static Texture2D StandaloneIcon = LoadIcon("Standalone");

        public static ReadOnlyCollection<BuildPlatform> AllBuildPlatforms
        {
            get { return _allBuildPlatforms.AsReadOnly(); }
        }

        private static readonly List<BuildPlatform> _allBuildPlatforms;

        static EditorUtils()
        {
            _allBuildPlatforms = new List<BuildPlatform>
            {
                new BuildPlatform("Web Player", WEB_PLAYER_PLATFORM_ID, true, LoadIcon(WEB_PLAYER_PLATFORM_ID))
            };
        }

        private static Texture2D LoadIcon(string iconId)
        {
            return EditorGUIUtility.IconContent(string.Format("BuildSettings.{0}.Small", iconId)).image as Texture2D;
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