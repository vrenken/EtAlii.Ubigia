// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage.Internal
{
    using System;
    using System.Linq;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Utilities;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     <para>
    ///         This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///         the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///         any release. You should only use it directly in your code with extreme caution and knowing that
    ///         doing so can result in application failures when updating to a new Entity Framework Core release.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Singleton" />. This means a single instance
    ///         is used by many <see cref="UbigiaDbContext" /> instances. The implementation must be thread-safe.
    ///         This service cannot depend on services registered as <see cref="ServiceLifetime.Scoped" />.
    ///     </para>
    /// </summary>
    public class UbigiaTypeMappingSource : TypeMappingSource
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaTypeMappingSource([NotNull] TypeMappingSourceDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override CoreTypeMapping FindMapping(in TypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            Check.DebugAssert(clrType != null, "ClrType is null");

            if (clrType.IsValueType
                || clrType == typeof(string)
                || clrType == typeof(byte[]))
            {
                return new UbigiaTypeMapping(clrType);
            }

            if (clrType.FullName == "NetTopologySuite.Geometries.Geometry"
                || clrType.GetBaseTypes().Any(t => t.FullName == "NetTopologySuite.Geometries.Geometry"))
            {
                var comparer = (ValueComparer)Activator.CreateInstance(typeof(GeometryValueComparer<>).MakeGenericType(clrType));

                return new UbigiaTypeMapping(
                    clrType,
                    comparer,
                    comparer);
            }

            return base.FindMapping(mappingInfo);
        }
    }
}
