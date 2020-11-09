// ReSharper disable all

using System;
using System.Diagnostics;
using System.Reflection;

namespace HashLib
{
    [DebuggerStepThrough]
    internal static class TypeExtensions
    {
        public static bool IsDerivedFrom(this Type a_type, Type a_baseType)
        {
            Debug.Assert(a_type != null);
            Debug.Assert(a_baseType != null);
            Debug.Assert(a_type.GetTypeInfo().IsClass);
            Debug.Assert(a_baseType.GetTypeInfo().IsClass);

            return a_baseType.GetTypeInfo().IsAssignableFrom(a_type.GetTypeInfo());
        }

        public static bool IsImplementInterface(this Type a_type, Type a_interfaceType)
        {
            Debug.Assert(a_type != null);
            Debug.Assert(a_interfaceType != null);
            Debug.Assert(a_type.GetTypeInfo().IsClass || a_type.GetTypeInfo().IsInterface || a_type.GetTypeInfo().IsValueType);
            Debug.Assert(a_interfaceType.GetTypeInfo().IsInterface);

            return a_interfaceType.GetTypeInfo().IsAssignableFrom(a_type.GetTypeInfo());
        }
    }
}