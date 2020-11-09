// ReSharper disable all

namespace TomanuExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public static class EnumExtensions
    {
        public static T Parse<T>(string @string)
        {
            return (T)Enum.Parse(typeof(T), @string);
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}