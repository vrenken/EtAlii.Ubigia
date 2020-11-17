// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Query.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using EtAlii.Ubigia.Api.Storage.Internal;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaQueryContext : QueryContext
    {
        private readonly IDictionary<IEntityType, IEnumerable<ValueBuffer>> _valueBuffersCache
            = new Dictionary<IEntityType, IEnumerable<ValueBuffer>>();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IEnumerable<ValueBuffer> GetValueBuffers([NotNull] IEntityType entityType)
        {
            if (!_valueBuffersCache.TryGetValue(entityType, out var valueBuffers))
            {
                valueBuffers = Store
                    .GetTables(entityType)
                    .SelectMany(t => t.Rows.Select(vs => new ValueBuffer(vs)))
                    .ToList();

                _valueBuffersCache[entityType] = valueBuffers;
            }

            return valueBuffers;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaQueryContext(
            [NotNull] QueryContextDependencies dependencies,
            [NotNull] IUbigiaStore store)
            : base(dependencies)
            => Store = store;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IUbigiaStore Store { get; }
    }
}
