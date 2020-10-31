// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.Utilities;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Diagnostics;
    using EtAlii.Ubigia.Api.Storage;

    /// <summary>
    ///     In-memory specific extension methods for <see cref="DbContextOptionsBuilder" />.
    /// </summary>
    public static class UbigiaDbContextOptionsExtensions
    {
        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <typeparam name="TContext"> The type of context being configured. </typeparam>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="databaseName">
        ///     The name of the in-memory database. This allows the scope of the in-memory database to be controlled
        ///     independently of the context. The in-memory database is shared anywhere the same name is used.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional in-memory specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseUbigiaContext<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseUbigiaContext(
                (DbContextOptionsBuilder)optionsBuilder, databaseName, ubigiaOptionsAction);

        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="databaseName">
        ///     The name of the in-memory database. This allows the scope of the in-memory database to be controlled
        ///     independently of the context. The in-memory database is shared anywhere the same name is used.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional in-memory specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseUbigiaContext(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            => UseUbigiaContext(optionsBuilder, databaseName, null, ubigiaOptionsAction);

        
        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        ///     passing a shared <see cref="UbigiaDatabaseRoot" /> on which to root the database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="databaseName">
        ///     The name of the in-memory database. This allows the scope of the in-memory database to be controlled
        ///     independently of the context. The in-memory database is shared anywhere the same name is used.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional in-memory specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseUbigiaContext<TTransport>(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TTransport : ITransport
            => UseUbigiaContext(optionsBuilder, databaseName, null, ubigiaOptionsAction);

        
        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <typeparam name="TContext"> The type of context being configured. </typeparam>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="databaseName">
        ///     The name of the in-memory database. This allows the scope of the in-memory database to be controlled
        ///     independently of the context. The in-memory database is shared anywhere the same name is used.
        /// </param>
        /// <param name="databaseRoot">
        ///     All in-memory databases will be rooted in this object, allowing the application
        ///     to control their lifetime. This is useful when sometimes the context instance
        ///     is created explicitly with <c>new</c> while at other times it is resolved using dependency injection.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional in-memory specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseUbigiaContext<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] UbigiaDatabaseRoot databaseRoot,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseUbigiaContext(
                (DbContextOptionsBuilder)optionsBuilder, databaseName, databaseRoot, ubigiaOptionsAction);

        
        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <typeparam name="TContext"> The type of context being configured. </typeparam>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="databaseName">
        ///     The name of the in-memory database. This allows the scope of the in-memory database to be controlled
        ///     independently of the context. The in-memory database is shared anywhere the same name is used.
        /// </param>
        /// <param name="databaseRoot">
        ///     All in-memory databases will be rooted in this object, allowing the application
        ///     to control their lifetime. This is useful when sometimes the context instance
        ///     is created explicitly with <c>new</c> while at other times it is resolved using dependency injection.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional in-memory specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseUbigiaContext<TContext, TTransport>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] UbigiaDatabaseRoot databaseRoot,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TContext : DbContext
            where TTransport : ITransport
            => (DbContextOptionsBuilder<TContext>)UseUbigiaContext(
                (DbContextOptionsBuilder)optionsBuilder, databaseName, databaseRoot, ubigiaOptionsAction);

        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="databaseName">
        ///     The name of the in-memory database. This allows the scope of the in-memory database to be controlled
        ///     independently of the context. The in-memory database is shared anywhere the same name is used.
        /// </param>
        /// <param name="databaseRoot">
        ///     All in-memory databases will be rooted in this object, allowing the application
        ///     to control their lifetime. This is useful when sometimes the context instance
        ///     is created explicitly with <c>new</c> while at other times it is resolved using dependency injection.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional in-memory specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseUbigiaContext(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string databaseName,
            [CanBeNull] UbigiaDatabaseRoot databaseRoot,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotEmpty(databaseName, nameof(databaseName));

            var extension = optionsBuilder.Options.FindExtension<UbigiaOptionsExtension>()
                ?? new UbigiaOptionsExtension();

            extension = extension.WithStoreName(databaseName);

            if (databaseRoot != null)
            {
                extension = extension.WithDatabaseRoot(databaseRoot);
            }

            ConfigureWarnings(optionsBuilder);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ubigiaOptionsAction?.Invoke(new UbigiaDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        private static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        {
            // Set warnings defaults
            var coreOptionsExtension
                = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()
                ?? new CoreOptionsExtension();

            coreOptionsExtension = coreOptionsExtension.WithWarningsConfiguration(
                coreOptionsExtension.WarningsConfiguration.TryWithExplicit(
                    UbigiaEventId.TransactionIgnoredWarning, WarningBehavior.Throw));

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        }
    }
}
