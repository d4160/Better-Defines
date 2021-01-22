using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BetterDefines.Editor.Entity
{
    public class BetterDefinesSettings : ScriptableObject
    {
        const string SettingsRelativePath = "Editor Resources/BetterDefinesSettings.asset";

        private static BetterDefinesSettings _instance;

        public List<CustomDefine> Defines;
        public List<PlatformEnabledState> EnabledPlatformsGlobal;

        public static BetterDefinesSettings Instance => _instance ? _instance : (_instance = AssetsUtilities.LoadSetting<BetterDefinesSettings>(SettingsRelativePath));

        public bool IsDefinePresent(string define)
        {
            return Defines.Any(x => x.Define == define);
        }

        public void UpdatePlatformOfDefine(int idx, string define, string selectedPlatformId)
        {
            if (idx < Defines.Count)
            {
                Defines[idx].Define = define;
                Defines[idx].UpdatePlatformList();
                
                if(!string.IsNullOrEmpty(selectedPlatformId))
                    SetDefineState(define, selectedPlatformId, true);
            }
            else
            {
                var cd = new CustomDefine("");
                cd.Define = define;
                Defines.Add(cd);
                
                if(!string.IsNullOrEmpty(selectedPlatformId))
                    SetDefineState(define, selectedPlatformId, true);
            }
        }

        #region global_platform_enables
        public PlatformEnabledState GetGlobalPlatformState(string platformId)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new InvalidOperationException("Incorrect platform platformId: " + platformId);
            }

            if (EnabledPlatformsGlobal.SingleOrDefault(x => x.PlatformId == platformId) != null)
            {
                return EnabledPlatformsGlobal.Single(x => x.PlatformId == platformId);
            }
            var toAdd = new PlatformEnabledState(platformId, false);
            EnabledPlatformsGlobal.Add(toAdd);
            return EnabledPlatformsGlobal.Single(x => x.PlatformId == platformId);
        }

        public List<string> GetGlobalUserEnabledPlatformIds()
        {
            return EnabledPlatformsGlobal.Where(x => x.IsEnabled).ToList().ConvertAll(x => x.PlatformId);
        } 
        #endregion

        #region defines
        public void SetDefineState(string define, string platformId, bool state)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new InvalidOperationException("Incorrect platform platformId: " + platformId);
            }

            var customDefine = Defines.SingleOrDefault(x => x.Define == define);
            if(customDefine == null)
            {
                customDefine = new CustomDefine(define);
                Defines.Add(customDefine);
            }

            customDefine.EnableForPlatform(platformId, state);
            EditorUtility.SetDirty(this);
        }

        public bool GetDefineState(string define, string platformId)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new InvalidOperationException("Incorrect platform platformId: " + platformId);
            }

            var defineSymbol = Defines.SingleOrDefault(x => x.Define == define);
            return defineSymbol != null && defineSymbol.IsPlatformEnabled(platformId);
        }
        #endregion

        public void SetScriptableDirty()
        {
            EditorUtility.SetDirty(this);
        }
    }
    
    public static class AssetsUtilities
    {
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            var assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            for( var i = 0; i < guids.Length; i++ )
            {
                string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );

                if(assetPath.StartsWith("Packages"))
                    continue;

                T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
                if(asset != null)
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        public static T LoadSetting<T>(string relativePath) where T : UnityEngine.Object
        {
            var assetPath = Path.Combine("Assets/", relativePath);
            // var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            T asset = null;
            var assets = FindAssetsByType<T>();
            if (assets.Count > 0)
            {
                asset = assets[0];

                if(assets.Count > 1)
                    Debug.LogWarning($"There is more than one instance of type {typeof(T).Name} in the Assets folder. {assets[0].name} was selected", assets[0]);
            }

            if (!asset)
            {
                BetterDefinesSettings newData =
                    ScriptableObject.CreateInstance(typeof(T)) as BetterDefinesSettings;

                string[] dirs = assetPath.Split('/');
                string allPath = dirs[0];
                for (int i = 1; i < dirs.Length - 1; ++i)
                {
                    if (!AssetDatabase.IsValidFolder(allPath + "/" + dirs[i]))
                    {
                        AssetDatabase.CreateFolder(allPath, dirs[i]);
                    }

                    allPath = allPath + "/" + dirs[i];
                }

                AssetDatabase.CreateAsset(newData, assetPath);
                AssetDatabase.SaveAssets();
                asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return asset;
        }
    }
}