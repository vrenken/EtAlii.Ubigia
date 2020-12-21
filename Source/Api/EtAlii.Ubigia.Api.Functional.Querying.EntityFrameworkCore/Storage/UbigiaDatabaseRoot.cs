// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.
#pragma warning disable S1104 // Fields should not have public accessibility.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    /// <summary>
    ///     Acts as a root for all Ubigia databases such that they will be available
    ///     across context instances and service providers as long as the same instance
    ///     of this type is passed to
    ///     <see
    ///         cref="UbigiaDbContextOptionsExtensions.UseUbigiaContext{TContext}(DbContextOptionsBuilder{TContext},string,string,string,string,System.Action{Infrastructure.UbigiaDbContextOptionsBuilder})" />
    /// </summary>
    public sealed class UbigiaDatabaseRoot
    {
        /// <summary>
        ///     <para>
        ///         This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///         the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///         any release. You should only use it directly in your code with extreme caution and knowing that
        ///         doing so can result in application failures when updating to a new Entity Framework Core release.
        ///     </para>
        ///     <para>
        ///         Entity Framework code will set this instance as needed. It should be considered opaque to
        ///         application code; the type of object may change at any time.
        ///     </para>
        /// </summary>
        [EntityFrameworkInternal]
        public object Instance;
    }
}
