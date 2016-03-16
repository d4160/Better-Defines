#if UNITY_4_4 || UNITY_4_5 || UNITY_4_5 || UNITY_4_7
#define UNITY_4
#endif

#if UNITY_4 || (UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3_0 || UNITY_5_3_1)
#define UNITY_PRE_5_3_2
#endif

#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#define UNITY_PRE_5_3_0
#endif

#if !UNITY_PRE_5_3_0
#define UNITY_5_3_0_AND_LATER
#endif

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
        public const string BLACKBERRY_PLATFORM_ID = "BlackBerry";
        public const string TIZEN_PLATFORM_ID = "Tizen";
        public const string XBOX360_PLATFORM_ID = "XBox360";
        public const string XBOX_ONE_PLATFORM_ID = "XboxOne";
        public const string PS3_PLATFORM_ID = "PS3";
        public const string PS_VITA_PLATFORM_ID = "PSP2";
        public const string PS4_PLATFORM_ID = "PS4";
        public const string WINDOWS_STORE_PLATFORM_ID = "Metro";
        public const string WP8_PLATFORM_ID = "WP8";
        public const string WEB_GL_PLATFORM_ID = "WebGL";
        public const string SAMSUNG_TV_PLATFORM_ID = "SamsungTV";
        public const string NINTENDO_3DS_PLATFORM_ID = "N3DS";
        public const string TV_OS_PLATFORM_ID = "tvOS";
        public const string WIIU_PLATFORM_ID = "WiiU";

        private static readonly List<BuildPlatform> AvailableBuildPlatforms;
        private static readonly Dictionary<string, BuildTargetGroup> BuildTargetGroups;

        static PlatformUtils()
        {
            AvailableBuildPlatforms = InitAvailableBuildPlatforms();
            BuildTargetGroups = InitBuildTargetGroupsDic();
        }

        private static List<BuildPlatform> InitAvailableBuildPlatforms()
        {
            return new List<BuildPlatform>
            {
                new BuildPlatform("Web Player", WEB_PLAYER_PLATFORM_ID, true, LoadPlatformIcon(WEB_PLAYER_PLATFORM_ID)),
                new BuildPlatform("PC, Mac & Linux Standalone", STANDALONE_PLATFORM_ID, true, LoadPlatformIcon(STANDALONE_PLATFORM_ID)),
                new BuildPlatform("Android", ANDROID_PLATFORM_ID, true, LoadPlatformIcon(ANDROID_PLATFORM_ID)),
                new BuildPlatform("iOS", IOS_PLATFORM_ID, true, LoadPlatformIcon(IOS_PLATFORM_ID)),
#if UNITY_5_3_0_AND_LATER
                new BuildPlatform("tvOS", TV_OS_PLATFORM_ID, true, LoadPlatformIcon(TV_OS_PLATFORM_ID)),
#endif

#if !UNITY_5_3_OR_NEWER // Unity directive added in https://unity3d.com/unity/whats-new/unity-5.3.4
                new BuildPlatform("BlackBerry", BLACKBERRY_PLATFORM_ID, true, LoadPlatformIcon(BLACKBERRY_PLATFORM_ID)),
#endif
                new BuildPlatform("Tizen", TIZEN_PLATFORM_ID, true, LoadPlatformIcon(TIZEN_PLATFORM_ID)),
                new BuildPlatform("Xbox 360", XBOX360_PLATFORM_ID, true, LoadPlatformIcon(XBOX360_PLATFORM_ID)),
                new BuildPlatform("Xbox One", XBOX_ONE_PLATFORM_ID, true, LoadPlatformIcon(XBOX_ONE_PLATFORM_ID)),
                new BuildPlatform("PS3", PS3_PLATFORM_ID, true, LoadPlatformIcon(PS3_PLATFORM_ID)),
                new BuildPlatform("PS Vita", PS_VITA_PLATFORM_ID, true, LoadPlatformIcon(PS_VITA_PLATFORM_ID)),
                new BuildPlatform("PS4", PS4_PLATFORM_ID, true, LoadPlatformIcon(PS4_PLATFORM_ID)),
#if UNITY_5_3_0_AND_LATER
                new BuildPlatform("Wii U", WIIU_PLATFORM_ID, true, LoadPlatformIcon(WIIU_PLATFORM_ID)),
                new BuildPlatform("Nintendo 3DS", NINTENDO_3DS_PLATFORM_ID, true, LoadPlatformIcon(NINTENDO_3DS_PLATFORM_ID)),
#endif

                new BuildPlatform("Windows Store", WINDOWS_STORE_PLATFORM_ID, true, LoadPlatformIcon(WINDOWS_STORE_PLATFORM_ID)),
#if UNITY_PRE_5_3_0
                new BuildPlatform("Windows Phone 8", WP8_PLATFORM_ID, true, LoadPlatformIcon(WP8_PLATFORM_ID)),
#endif
#if UNITY_5 // WebGL appeared in Unity 5
                new BuildPlatform("WebGL", WEB_GL_PLATFORM_ID, true, LoadPlatformIcon(WEB_GL_PLATFORM_ID)),
#endif
                new BuildPlatform("Samsung TV", SAMSUNG_TV_PLATFORM_ID, true, LoadPlatformIcon(SAMSUNG_TV_PLATFORM_ID)),
            };
        }

        private static Dictionary<string, BuildTargetGroup> InitBuildTargetGroupsDic()
        {
            return new Dictionary<string, BuildTargetGroup>
            {
                {WEB_PLAYER_PLATFORM_ID, BuildTargetGroup.WebPlayer},
                {STANDALONE_PLATFORM_ID, BuildTargetGroup.Standalone},
                {ANDROID_PLATFORM_ID, BuildTargetGroup.Android},
#if !UNITY_PRE_5_3_2
                {TV_OS_PLATFORM_ID, BuildTargetGroup.tvOS},
                {NINTENDO_3DS_PLATFORM_ID, BuildTargetGroup.Nintendo3DS},
                {WIIU_PLATFORM_ID, BuildTargetGroup.WiiU},
#endif

#if UNITY_4
                { IOS_PLATFORM_ID, BuildTargetGroup.iPhone },
#else
                {IOS_PLATFORM_ID, BuildTargetGroup.iOS},
#endif

                {BLACKBERRY_PLATFORM_ID, BuildTargetGroup.BlackBerry},
                {TIZEN_PLATFORM_ID, BuildTargetGroup.Tizen},
                {XBOX360_PLATFORM_ID, BuildTargetGroup.XBOX360},
                {XBOX_ONE_PLATFORM_ID, BuildTargetGroup.XboxOne},
                {PS3_PLATFORM_ID, BuildTargetGroup.PS3},
                {PS_VITA_PLATFORM_ID, BuildTargetGroup.PSP2},
                {PS4_PLATFORM_ID, BuildTargetGroup.PS4},
#if UNITY_5
                {WINDOWS_STORE_PLATFORM_ID, BuildTargetGroup.WSA},
                {WEB_GL_PLATFORM_ID, BuildTargetGroup.WebGL},
#endif

#if UNITY_PRE_5_3_0
                { WP8_PLATFORM_ID, BuildTargetGroup.WP8 },
#endif
                {SAMSUNG_TV_PLATFORM_ID, BuildTargetGroup.SamsungTV},
            };
        }

        public static ReadOnlyCollection<BuildPlatform> AllAvailableBuildPlatforms
        {
            get { return AvailableBuildPlatforms.AsReadOnly(); }
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
            return BuildTargetGroups.ContainsKey(platformId) ? BuildTargetGroups[platformId] : BuildTargetGroup.Unknown;
        }
    }
}