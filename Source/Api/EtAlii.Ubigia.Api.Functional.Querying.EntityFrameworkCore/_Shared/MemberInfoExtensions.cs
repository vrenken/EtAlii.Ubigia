// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S3358 // This code will change. remove this pragma afterwards.
#pragma warning disable S1125 // Boolean literals should not be redundant

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    using System.Linq;

    internal static class MemberInfoExtensions
    {
        public static Type GetMemberType(this MemberInfo memberInfo)
            => (memberInfo as PropertyInfo)?.PropertyType ?? ((FieldInfo)memberInfo)?.FieldType;

        public static bool IsSameAs(this MemberInfo propertyInfo, MemberInfo otherPropertyInfo)
            => propertyInfo == null
                ? otherPropertyInfo == null
                : otherPropertyInfo != null && (Equals(propertyInfo, otherPropertyInfo)
                                                || propertyInfo.Name == otherPropertyInfo.Name
                                                && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType
                                                    || propertyInfo.DeclaringType.GetTypeInfo().IsSubclassOf(otherPropertyInfo.DeclaringType!)
                                                    || otherPropertyInfo.DeclaringType.GetTypeInfo().IsSubclassOf(propertyInfo.DeclaringType!)
                                                    || propertyInfo.DeclaringType.GetTypeInfo().ImplementedInterfaces.Contains(otherPropertyInfo.DeclaringType)
                                                    || otherPropertyInfo.DeclaringType.GetTypeInfo().ImplementedInterfaces
                                                        .Contains(propertyInfo.DeclaringType)));

        public static bool IsOverridenBy(this MemberInfo propertyInfo, MemberInfo otherPropertyInfo)
            => propertyInfo == null
                ? otherPropertyInfo == null
                : (otherPropertyInfo == null
                    ? false
                    : Equals(propertyInfo, otherPropertyInfo)
                    || (propertyInfo.Name == otherPropertyInfo.Name
                        && (propertyInfo.DeclaringType == otherPropertyInfo.DeclaringType
                            || otherPropertyInfo.DeclaringType.GetTypeInfo().IsSubclassOf(propertyInfo.DeclaringType!)
                            || otherPropertyInfo.DeclaringType.GetTypeInfo().ImplementedInterfaces
                                .Contains(propertyInfo.DeclaringType))));

        public static string GetSimpleMemberName(this MemberInfo member)
        {
            var name = member.Name;
            var index = name.LastIndexOf('.');
            return index >= 0 ? name.Substring(index + 1) : name;
        }

        public static bool IsReallyVirtual(this MethodInfo method)
            => method.IsVirtual && !method.IsFinal;
    }
}
