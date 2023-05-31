using System.Text.RegularExpressions;

namespace Api.Helpers
{
    public static partial class StringUtils
    {
        [GeneratedRegex("([a-z])([A-Z])")]
        private static partial Regex Regex();

        public static string? ToKebabCase(string? stringValue) =>
            string.IsNullOrEmpty(stringValue) ? null : Regex().Replace(stringValue, "$1-$2").ToLower();
    }

}

