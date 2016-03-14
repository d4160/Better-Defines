using System;
using System.CodeDom.Compiler;
using System.Linq;

namespace BetterDefines.Editor
{
    public static class ValidationUtils
    {
        private static readonly CodeDomProvider Provider = CodeDomProvider.CreateProvider("C#");

        public static bool IsValidDefineName(this string defineName)
        {
            return Provider.IsValidIdentifier(defineName);
        }

        public static bool IsValidBuildPlatformId(this string platformId)
        {
            return !String.IsNullOrEmpty(platformId) && EditorUtils.AllBuildPlatforms.Any(x => x.Id == platformId);
        }
    }
}