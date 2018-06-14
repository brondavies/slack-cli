using System;

namespace slack.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsUrl(this string value)
        {
            return !value.IsNull() && Uri.TryCreate(value, UriKind.Absolute, out Uri ignored);
        }

        public static bool FileExists(this string value)
        {
            return !value.IsNull() && System.IO.File.Exists(value);
        }

        public static string ReadAllText(this string value)
        {
            return System.IO.File.ReadAllText(value);
        }
    }
}
