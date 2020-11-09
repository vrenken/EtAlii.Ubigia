// ReSharper disable all

using System;
using System.Diagnostics;
using System.Reflection;

namespace TomanuExtensions
{
    [DebuggerStepThrough]
    public static class ConstructorInfoExtensions
    {
        public static object Invoke(this ConstructorInfo a_ci)
        {
            return a_ci.Invoke(null);
        }

        public static object Invoke(this ConstructorInfo a_ci, params object[] a_params)
        {
            return a_ci.Invoke(a_params);
        }
    }
}