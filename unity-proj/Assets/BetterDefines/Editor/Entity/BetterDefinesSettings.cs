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

        public PlatformEnabledState GetGlobalPlatformState(string id)
        {
            if (string.IsNullOrEmpty(id) || EditorUtils.AllBuildPlatforms.All(x => x.Id != id))
            {
                throw new InvalidOperationException("Incorrect platform id: " + id);
            }

            return EnabledPlatformsGlobal.Single(x => x.PlatformId == id);
        }
    }
}