using System.Collections.Generic;
using UnityEditor;

namespace BetterDefines.Editor.Entity
{
    [System.Serializable]
    public class CustomDefine
    {
        public string Define;
        public List<PlatformEnabledState> statesForPlatforms;
    }
}
