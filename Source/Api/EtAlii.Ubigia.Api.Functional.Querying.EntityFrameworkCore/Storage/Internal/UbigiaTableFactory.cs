// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage.Internal
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaTableFactory : IUbigiaTableFactory
    {
        private readonly bool _sensitiveLoggingEnabled;

        private readonly ConcurrentDictionary<(IEntityType EntityType, IUbigiaTable BaseTable), Func<IUbigiaTable>> _factories
            = new();

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
        public virtual IUbigiaTable Create(IEntityType entityType, IUbigiaTable baseTable)
            => _factories.GetOrAdd((entityType, baseTable), e => CreateTable(e.EntityType, e.BaseTable))();

        private Func<IUbigiaTable> CreateTable([NotNull] IEntityType entityType, IUbigiaTable baseTable)
            => (Func<IUbigiaTable>)typeof(UbigiaTableFactory).GetTypeInfo()
                .GetDeclaredMethod(nameof(CreateFactory))
                .MakeGenericMethod(entityType.FindPrimaryKey().GetKeyType())
                .Invoke(null, new object[] { entityType, baseTable, _sensitiveLoggingEnabled });

        [UsedImplicitly]
        private static Func<IUbigiaTable> CreateFactory<TKey>(
            IEntityType entityType,
            IUbigiaTable baseTable,
            bool sensitiveLoggingEnabled)
            => () => new UbigiaTable<TKey>(entityType, baseTable, sensitiveLoggingEnabled);
    }
}
