using System;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string TrimLeadingString(this string sourceString, string removeString)
        {
            int index = sourceString.IndexOf(removeString, StringComparison.CurrentCulture);
            string cleanPath = index < 0
                ? sourceString
                : sourceString.Remove(index, removeString.Length);
            return cleanPath;
        }

        public static string Replace(this string sourceString, string targetString, string replacementString)
        {
            int index = sourceString.IndexOf(targetString, StringComparison.CurrentCulture);
            return index < 0 ? sourceString : sourceString.Remove(index, targetString.Length).Insert(index, replacementString);
        }

        public static double? ToDouble(this string sourceString)
        {
            bool tryParse = double.TryParse(sourceString, out double value);
            return tryParse ? value : (double?)null;
        }
        public static int? ToInt(this string sourceString)
        {
            bool tryParse = int.TryParse(sourceString, out int value);
            return tryParse ? value : (int?)null;
        }

        public static string SplitCamelCase(this string sourceString)
        {
            return Regex.Replace(
                Regex.Replace(
                    sourceString,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
    }
}