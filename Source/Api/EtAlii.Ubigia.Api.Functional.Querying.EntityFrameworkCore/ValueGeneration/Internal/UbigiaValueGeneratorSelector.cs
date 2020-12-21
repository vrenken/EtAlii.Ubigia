// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.ValueGeneration.Internal
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Utilities;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     <para>
    ///         This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///         the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///         any release. You should only use it directly in your code with extreme caution and knowing that
    ///         doing so can result in application failures when updating to a new Entity Framework Core release.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Scoped" />. This means that each
    ///         <see cref="DbContext" /> instance will use its own instance of this service.
    ///         The implementation may depend on other services registered with any lifetime.
    ///         The implementation does not need to be thread-safe.
    ///     </para>
    /// </summary>
    public class UbigiaValueGeneratorSelector : ValueGeneratorSelector
    {
        private readonly IUbigiaStore _ubigiaStore;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaValueGeneratorSelector(
            [NotNull] ValueGeneratorSelectorDependencies dependencies,
            [NotNull] IUbigiaDatabase ubigiaDatabase)
            : base(dependencies)
        {
            _ubigiaStore = ubigiaDatabase.Store;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public override ValueGenerator Select(IProperty property, IEntityType entityType)
        {
            Check.NotNull(property, nameof(property));
            Check.NotNull(entityType, nameof(entityType));

            return property.GetValueGeneratorFactory() == null
                && property.ClrType.IsInteger()
                && property.ClrType.UnwrapNullableType() != typeof(char)
                    ? GetOrCreate(property)
                    : base.Select(property, entityType);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        private ValueGenerator GetOrCreate(IProperty property)
        {
            Check.NotNull(property, nameof(property));

            var type = property.ClrType.UnwrapNullableType().UnwrapEnumType();

            if (type == typeof(long))
            {
                return _ubigiaStore.GetIntegerValueGenerator<long>(property);
            }

            if (type == typeof(int))
            {
                return _ubigiaStore.GetIntegerValueGenerator<int>(property);
            }

            if (type == typeof(short))
            {
                return _ubigiaStore.GetIntegerValueGenerator<short>(property);
            }

            if (type == typeof(byte))
            {
                return _ubigiaStore.GetIntegerValueGenerator<byte>(property);
            }

            if (type == typeof(ulong))
            {
                return _ubigiaStore.GetIntegerValueGenerator<ulong>(property);
            }

            if (type == typeof(uint))
            {
                return _ubigiaStore.GetIntegerValueGenerator<uint>(property);
            }

            if (type == typeof(ushort))
            {
                return _ubigiaStore.GetIntegerValueGenerator<ushort>(property);
            }

            if (type == typeof(sbyte))
            {
                return _ubigiaStore.GetIntegerValueGenerator<sbyte>(property);
            }

            throw new ArgumentException(
                CoreStrings.InvalidValueGeneratorFactoryProperty(
                    "UbigiaIntegerValueGeneratorFactory", property.Name, property.DeclaringEntityType.DisplayName()));
        }
    }
}
