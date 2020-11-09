// ReSharper disable all

namespace TomanuExtensions
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        public static double ToDouble(this string a_str)
        {
            if (a_str.ToLower() == double.NegativeInfinity.ToString().ToLower())
                return double.NegativeInfinity;
            if (a_str.ToLower() == double.PositiveInfinity.ToString().ToLower())
                return double.PositiveInfinity;
            if (a_str.ToLower() == double.NaN.ToString().ToLower())
                return double.NaN;
            else
                return double.Parse(a_str, CultureInfo.InvariantCulture);
        }

        public static float ToSingle(this string a_str)
        {
            if (a_str.ToLower() == float.NegativeInfinity.ToString().ToLower())
                return float.NegativeInfinity;
            if (a_str.ToLower() == float.PositiveInfinity.ToString().ToLower())
                return float.PositiveInfinity;
            if (a_str.ToLower() == float.NaN.ToString().ToLower())
                return float.NaN;
            else
                return float.Parse(a_str, CultureInfo.InvariantCulture);
        }

        public static int ToInt(this string a_str)
        {
            return int.Parse(a_str);
        }

        public static bool ToBool(this string a_str)
        {
            return bool.Parse(a_str);
        }

        public static string RemoveFromRight(this string a_str, int a_chars)
        {
            return a_str.Remove(a_str.Length - a_chars);
        }

        public static string RemoveFromLeft(this string a_str, int a_chars)
        {
            return a_str.Remove(0, a_chars);
        }

        public static string Left(this string a_str, int a_count)
        {
            return a_str.Substring(0, a_count);
        }

        public static string Right(this string a_str, int a_count)
        {
            return a_str.Substring(a_str.Length - a_count, a_count);
        }

        public static string EnsureStartsWith(this string a_str, string a_prefix)
        {
            return a_str.StartsWith(a_prefix) ? a_str : string.Concat(a_prefix, a_str);
        }

        public static string Repeat(this string a_str, int a_count)
        {
            var sb = new StringBuilder(a_str.Length * a_count);

            for (var i = 0; i < a_count; i++)
                sb.Append(a_str);

            return sb.ToString();
        }

        public static string GetBefore(this string a_str, string a_pattern)
        {
            var index = a_str.IndexOf(a_pattern);
            return (index == -1) ? string.Empty : a_str.Substring(0, index);
        }

        public static string GetAfter(this string a_str, string a_pattern)
        {
            var last_pos = a_str.LastIndexOf(a_pattern);

            if (last_pos == -1)
                return string.Empty;

            var start = last_pos + a_pattern.Length;
            return start >= a_str.Length ? string.Empty : a_str.Substring(start).Trim();
        }

        public static string GetBetween(this string a_str, string a_left, string a_right)
        {
            return a_str.GetBefore(a_right).GetAfter(a_left);
        }

        public static string ToTitleCase(this string value)
        {
            return System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value);
        }

        public static string FindUniqueName(this string a_pattern,
            IEnumerable<string> a_names)
        {
            if (!a_names.Contains(a_pattern))
                return a_pattern;

            var ar = a_pattern.Split(new[] { ' ' });

            string left;
            uint index;
            if (!uint.TryParse(ar.Last(), out index))
            {
                index = 1;
                left = a_pattern + " ";
            }
            else
            {
                left = string.Join(" ", ar.SkipLast(1)) + " ";
                index++;
            }

            for (; ; )
            {
                var result = (left + index).Trim();

                if (a_names.Contains(result))
                {
                    index++;
                    continue;
                }

                return result;
            }
        }

        public static IEnumerable<string> Split(this string a_str, string a_split)
        {
            var start_index = 0;

            for (; ; )
            {
                var split_index = a_str.IndexOf(a_split, start_index);

                if (split_index == -1)
                    break;

                yield return a_str.Substring(start_index, split_index - start_index);

                start_index = split_index + a_split.Length;
            }

            yield return a_str.Substring(start_index);
        }
    }
}