using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BetterDefines.Editor.Entity
{
    public class BetterDefinesSettings : ScriptableObject
    {
        public const string SETTINGS_RESOURCE_NAME = "BetterDefinesSettings";

        private static BetterDefinesSettings _instance;

        public List<CustomDefine> Defines;
        public List<PlatformEnabledState> EnabledPlatformsGlobal;

        public static BetterDefinesSettings Instance
        {
            get { return _instance ?? (_instance = Resources.Load<BetterDefinesSettings>(SETTINGS_RESOURCE_NAME)); }
        }

        public PlatformEnabledState GetGlobalPlatformState(string platformId)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new InvalidOperationException("Incorrect platform platformId: " + platformId);
            }

            return EnabledPlatformsGlobal.Single(x => x.PlatformId == platformId);
        }

        public void SetDefineState(string define, string platformId, bool state)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new InvalidOperationException("Incorrect platform platformId: " + platformId);
            }

            var customDefine = Defines.Single(x => x.Define == define);
            customDefine.EnableForPlatform(platformId, state);
        }

        public bool GetDefineState(string define, string platformId)
        {
            if (!platformId.IsValidBuildPlatformId())
            {
                throw new InvalidOperationException("Incorrect platform platformId: " + platformId);
            }

            return Defines.Single(x => x.Define == define).IsPlatformEnabled(platformId);
        }
    }
}