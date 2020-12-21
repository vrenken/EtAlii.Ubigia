// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable once CheckNamespace

using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal;

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal;

    /// <summary>
    ///     Ubigia specific extension methods for <see cref="UbigiaDbContext.Database" />.
    /// </summary>
    public static class UbigiaDatabaseFacadeExtensions
    {
        /// <summary>
        ///     <para>
        ///         Returns <see langword="true" /> if the database provider currently in use is the Ubigia provider.
        ///     </para>
        ///     <para>
        ///         This method can only be used after the <see cref="UbigiaDbContext" /> has been configured because
        ///         it is only then that the provider is known. This means that this method cannot be used
        ///         in <see cref="UbigiaDbContext.OnConfiguring" /> because this is where application code sets the
        ///         provider to use as part of configuring the context.
        ///     </para>
        /// </summary>
        /// <param name="database"> The facade from <see cref="UbigiaDbContext.Database" />. </param>
        /// <returns> <see langword="true" /> if the Ubigia database is being used. </returns>
        public static bool IsUbigia([NotNull] this DatabaseFacade database)
            => database.ProviderName.Equals(
                typeof(UbigiaOptionsExtension).Assembly.GetName().Name,
                StringComparison.Ordinal);
    }
}