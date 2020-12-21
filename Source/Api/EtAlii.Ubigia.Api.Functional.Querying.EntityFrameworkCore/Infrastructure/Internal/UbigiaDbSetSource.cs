// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.
#pragma warning disable S1172 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;

    /// <summary>
    ///     <para>
    ///         This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///         the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///         any release. You should only use it directly in your code with extreme caution and knowing that
    ///         doing so can result in application failures when updating to a new Entity Framework Core release.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Singleton" />. This means a single instance
    ///         is used by many <see cref="DbContext" /> instances. The implementation must be thread-safe.
    ///         This service cannot depend on services registered as <see cref="ServiceLifetime.Scoped" />.
    ///     </para>
    /// </summary>
#pragma warning disable EF1001
    public class UbigiaDbSetSource : IDbSetSource
#pragma warning restore EF1001
    {
        private static readonly MethodInfo _genericCreateSet = typeof(UbigiaDbSetSource).GetTypeInfo().GetDeclaredMethod(nameof(CreateSetFactory));

        private readonly ConcurrentDictionary<(Type Type, string Name), Func<DbContext, string, object>> _cache = new();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual object Create(DbContext context, Type type) => CreateCore(context, type, null, _genericCreateSet);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual object Create(DbContext context, string name, Type type) => CreateCore(context, type, name, _genericCreateSet);

        private object CreateCore(DbContext context, Type type, string name, MethodInfo createMethod)
            => _cache.GetOrAdd(
                (type, name),
                t => (Func<DbContext, string, object>)createMethod
                    .MakeGenericMethod(t.Type)
                    .Invoke(null, null))(context, name);

        [UsedImplicitly]
        private static Func<DbContext, string, object> CreateSetFactory<TEntity>()
            where TEntity : class
            => (c, name) => new UbigiaInternalDbSet<TEntity>(c, name);
    }
}
