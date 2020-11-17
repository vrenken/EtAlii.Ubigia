// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Storage.Internal
{
    using System.Linq;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Infrastructure.Internal;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static class UbigiaStoreCacheExtensions
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public static IUbigiaStore GetStore([NotNull] this IUbigiaStoreCache storeCache, [NotNull] IDbContextOptions options)
            => storeCache.GetStore(options.Extensions.OfType<UbigiaOptionsExtension>().First().Storage);
    }
}