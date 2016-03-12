using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BetterDefines.Editor.Entity
{
    [System.Serializable]
    public class CustomDefine
    {
        public string Define;
        public List<PlatformEnabledState> StatesForPlatforms;

        public bool IsPlatformEnabled(string platformId)
        {
            if (string.IsNullOrEmpty(platformId))
            {
                throw new ArgumentNullException("Platform Id");
            }

            if (StatesForPlatforms.All(x => x.PlatformId != platformId))
            {
                Debug.LogWarning("Platform id not available. Adding it to " + Define);
                // TODO Change to be initialized correctly when added by (+)
                StatesForPlatforms.Add(new PlatformEnabledState(platformId, false));
            }

            return StatesForPlatforms.Single(x => x.PlatformId == platformId).IsEnabled;
        }

        public void EnableForPlatform(string platformId, bool isEnabled)
        {
            StatesForPlatforms.Single(x => x.PlatformId == platformId).IsEnabled = isEnabled;
        }
    }
}
