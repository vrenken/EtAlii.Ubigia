// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Storage.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Utilities;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaTableFactory
        // WARNING: The in-memory provider is using EF internal code here. This should not be copied by other providers. See #15096
#pragma warning disable EF1001 // Internal API
        : IdentityMapFactoryFactoryBase, IUbigiaTableFactory
#pragma warning restore EF1001 // Internal API
   
    {
        private readonly bool _sensitiveLoggingEnabled;

        private readonly ConcurrentDictionary<IKey, Func<IUbigiaTable>> _factories
            = new ConcurrentDictionary<IKey, Func<IUbigiaTable>>();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaTableFactory([NotNull] ILoggingOptions loggingOptions)
        {
            Check.NotNull(loggingOptions, nameof(loggingOptions));

            _sensitiveLoggingEnabled = loggingOptions.IsSensitiveDataLoggingEnabled;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IUbigiaTable Create(IEntityType entityType)
            => _factories.GetOrAdd(entityType.FindPrimaryKey(), Create)();

        private Func<IUbigiaTable> Create([NotNull] IKey key)
            => (Func<IUbigiaTable>)typeof(UbigiaTableFactory).GetTypeInfo()
                .GetDeclaredMethod(nameof(CreateFactory))
                .MakeGenericMethod(GetKeyType(key))
                .Invoke(null, new object[] { key, _sensitiveLoggingEnabled });

        [UsedImplicitly]
        private static Func<IUbigiaTable> CreateFactory<TKey>(IKey key, bool sensitiveLoggingEnabled)
            => () => new UbigiaTable<TKey>(
                // WARNING: The in-memory provider is using EF internal code here. This should not be copied by other providers. See #15096
#pragma warning disable EF1001 // Internal API
                Microsoft.EntityFrameworkCore.Metadata.Internal.KeyExtensions.GetPrincipalKeyValueFactory<TKey>(key),
#pragma warning restore EF1001 // Internal API
                sensitiveLoggingEnabled);
    }
}
