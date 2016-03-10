using System.Collections.Generic;
using UnityEditor;

namespace BetterDefines.Editor
{
    public static class BetterDefinesUtils
    {
        private static void ToggleFlag(string targetFlag, bool enable, params BuildTargetGroup[] supportedPlatforms)
        {
            foreach (var targetPlatform in supportedPlatforms)
            {
                var scriptDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetPlatform);
                var flags = new List<string>(scriptDefines.Split(';'));

                if (flags.Contains(targetFlag))
                {
                    if (!enable)
                    {
                        flags.Remove(targetFlag);
                    }
                }
                else
                {
                    if (enable)
                    {
                        flags.Add(targetFlag);
                    }
                }

                var result = string.Join(";", flags.ToArray());

                if (scriptDefines != result)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(targetPlatform, result);
                }
            }
        }
    }
}
