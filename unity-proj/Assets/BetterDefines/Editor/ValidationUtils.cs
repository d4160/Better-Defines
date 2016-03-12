using System.CodeDom.Compiler;

namespace BetterDefines.Editor
{
    public static class ValidationUtils
    {
        private static readonly CodeDomProvider Provider = CodeDomProvider.CreateProvider("C#");

        public static bool IsValidDefineName(this string defineName)
        {
            return Provider.IsValidIdentifier(defineName);
        }
    }
}