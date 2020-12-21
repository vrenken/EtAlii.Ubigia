// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal;

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage.Internal
{
    using System.Collections.Concurrent;
    using System.Threading;
    using JetBrains.Annotations;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaStoreCache : IUbigiaStoreCache
    {
        private readonly IUbigiaTableFactory _tableFactory;
        private readonly bool _useNameMatching;
        private readonly ConcurrentDictionary<string, IUbigiaStore> _namedStores;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaStoreCache(
            [NotNull] IUbigiaTableFactory tableFactory,
            [CanBeNull] IUbigiaSingletonOptions options)
        {
            _tableFactory = tableFactory;

            if (options?.DatabaseRoot != null)
            {
                _useNameMatching = true;

                LazyInitializer.EnsureInitialized(
                    ref options.DatabaseRoot.Instance,
                    () => new ConcurrentDictionary<string, IUbigiaStore>());

                _namedStores = (ConcurrentDictionary<string, IUbigiaStore>)options.DatabaseRoot.Instance;
            }
            else
            {
                _namedStores = new ConcurrentDictionary<string, IUbigiaStore>();
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IUbigiaStore GetStore(string name)
            => _namedStores.GetOrAdd(name, _ => new UbigiaStore(_tableFactory, _useNameMatching));
    }
}
