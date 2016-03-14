using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BetterDefines.Editor.Entity;
using UnityEditor;
using UnityEngine;

namespace BetterDefines.Editor
{
    [InitializeOnLoad]
    public static class PlatformUtils
    {
        public const string WEB_PLAYER_PLATFORM_ID = "Web";
        public const string STANDALONE_PLATFORM_ID = "Standalone";
        public const string ANDROID_PLATFORM_ID = "Android";
        public const string IOS_PLATFORM_ID = "iPhone";
        public const string TV_OS_PLATFORM_ID = "tvOS";
        public const string BLACKBERRY_PLATFORM_ID = "BlackBerry";
        public const string TIZEN_PLATFORM_ID = "Tizen";
        public const string XBOX360_PLATFORM_ID = "XBox360";
        public const string XBOX_ONE_PLATFORM_ID = "XboxOne";
        public const string PS3_PLATFORM_ID = "PS3";
        public const string PS_VITA_PLATFORM_ID = "PSP2";
        public const string PS4_PLATFORM_ID = "PS4";
        public const string GLESEMU_PLATFORM_ID = "StandaloneGLESEmu";
        public const string WIIU_PLATFORM_ID = "WiiU";
        public const string WINDOWS_STORE_PLATFORM_ID = "Metro";
        public const string WEB_GL_PLATFORM_ID = "WebGL";
        public const string SAMSUNG_TV_PLATFORM_ID = "SamsungTV";
        public const string NINTENDO_3DS_PLATFORM_ID = "N3DS";

        private static readonly List<BuildPlatform> _allBuildPlatforms;
        private static readonly Dictionary<string, BuildTargetGroup> _buildTargetGroups;

        static PlatformUtils()
        {
            _allBuildPlatforms = new List<BuildPlatform>
            {
                new BuildPlatform("Web Player", WEB_PLAYER_PLATFORM_ID, true, LoadPlatformIcon(WEB_PLAYER_PLATFORM_ID)),
                new BuildPlatform("PC, Mac & Linux Standalone", STANDALONE_PLATFORM_ID, true, LoadPlatformIcon(STANDALONE_PLATFORM_ID)),
                new BuildPlatform("Android", ANDROID_PLATFORM_ID, true, LoadPlatformIcon(ANDROID_PLATFORM_ID)),
                new BuildPlatform("iOS", IOS_PLATFORM_ID, true, LoadPlatformIcon(IOS_PLATFORM_ID)),
                new BuildPlatform("tvOS", TV_OS_PLATFORM_ID, true, LoadPlatformIcon(TV_OS_PLATFORM_ID)),
                new BuildPlatform("BlackBerry", BLACKBERRY_PLATFORM_ID, true, LoadPlatformIcon(BLACKBERRY_PLATFORM_ID)),
                new BuildPlatform("Tizen", TIZEN_PLATFORM_ID, true, LoadPlatformIcon(TIZEN_PLATFORM_ID)),
                new BuildPlatform("Xbox 360", XBOX360_PLATFORM_ID, true, LoadPlatformIcon(XBOX360_PLATFORM_ID)),
                new BuildPlatform("Xbox One", XBOX_ONE_PLATFORM_ID, true, LoadPlatformIcon(XBOX_ONE_PLATFORM_ID)),
                new BuildPlatform("PS3", PS3_PLATFORM_ID, true, LoadPlatformIcon(PS3_PLATFORM_ID)),
                new BuildPlatform("PS Vita", PS_VITA_PLATFORM_ID, true, LoadPlatformIcon(PS_VITA_PLATFORM_ID)),
                new BuildPlatform("PS4", PS4_PLATFORM_ID, true, LoadPlatformIcon(PS4_PLATFORM_ID)),
                // TODO GLES Emulator - what is this?
                //new BuildPlatform("GLES Emulator", GLESEMU_PLATFORM_ID, true, LoadPlatformIcon(GLESEMU_PLATFORM_ID)),
                new BuildPlatform("Wii U", WIIU_PLATFORM_ID, true, LoadPlatformIcon(WIIU_PLATFORM_ID)),
                new BuildPlatform("Windows Store", WINDOWS_STORE_PLATFORM_ID, true, LoadPlatformIcon(WINDOWS_STORE_PLATFORM_ID)),
                new BuildPlatform("WebGL", WEB_GL_PLATFORM_ID, true, LoadPlatformIcon(WEB_GL_PLATFORM_ID)),
                new BuildPlatform("Samsung TV", SAMSUNG_TV_PLATFORM_ID, true, LoadPlatformIcon(SAMSUNG_TV_PLATFORM_ID)),
                new BuildPlatform("Nintendo 3DS", NINTENDO_3DS_PLATFORM_ID, true, LoadPlatformIcon(NINTENDO_3DS_PLATFORM_ID))
            };
            _buildTargetGroups = new Dictionary<string, BuildTargetGroup>
            {
                {WEB_PLAYER_PLATFORM_ID, BuildTargetGroup.WebPlayer},
                {STANDALONE_PLATFORM_ID, BuildTargetGroup.Standalone},
                {ANDROID_PLATFORM_ID, BuildTargetGroup.Android},
                {IOS_PLATFORM_ID, BuildTargetGroup.iOS},
                {TV_OS_PLATFORM_ID, BuildTargetGroup.tvOS},
#if !UNITY_5
                { IOS_PLATFORM_ID, BuildTargetGroup.iPhone },
#endif
                {BLACKBERRY_PLATFORM_ID, BuildTargetGroup.BlackBerry},
                {TIZEN_PLATFORM_ID, BuildTargetGroup.Tizen},
                {XBOX360_PLATFORM_ID, BuildTargetGroup.XBOX360},
                {XBOX_ONE_PLATFORM_ID, BuildTargetGroup.XboxOne},
                {PS3_PLATFORM_ID, BuildTargetGroup.PS3},
                {PS_VITA_PLATFORM_ID, BuildTargetGroup.PSP2},
                {PS4_PLATFORM_ID, BuildTargetGroup.PS4},
                {WIIU_PLATFORM_ID, BuildTargetGroup.WiiU},
                {WINDOWS_STORE_PLATFORM_ID, BuildTargetGroup.WSA},
                {WEB_GL_PLATFORM_ID, BuildTargetGroup.WebGL},
                {SAMSUNG_TV_PLATFORM_ID, BuildTargetGroup.SamsungTV},
                {NINTENDO_3DS_PLATFORM_ID, BuildTargetGroup.Nintendo3DS}
            };
        }

        private static void InitBuildtTargetGroupsDic()
        {
            
        }

        public static ReadOnlyCollection<BuildPlatform> AllBuildPlatforms
        {
            get { return _allBuildPlatforms.AsReadOnly(); }
        }

        private static Texture2D LoadPlatformIcon(string iconId)
        {
            return EditorGUIUtility.IconContent(string.Format("BuildSettings.{0}.Small", iconId)).image as Texture2D;
        }

        public static BuildTargetGroup GetBuildTargetGroupById(string platformId)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new ArgumentException("Invalid platform id");
            }

            Debug.Log(platformId);

            // TODO return required value
            return _buildTargetGroups[platformId];
        }
    }
}