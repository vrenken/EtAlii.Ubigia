// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.Utilities;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Internal;

    /// <summary>
    ///     Ubigia specific extension methods for <see cref="DbContextOptionsBuilder" />.
    /// </summary>
    public static class UbigiaDbContextOptionsExtensions
    {
        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <typeparam name="TContext"> The type of context being configured. </typeparam>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="address">
        ///     The address of the Ubigia database. 
        /// </param>
        /// <param name="storage">
        ///     The storage that should be queried.
        /// </param>
        /// <param name="username">
        ///     The username with which to access the storage.
        /// </param>
        /// <param name="password">
        ///     The password with which to access the storage.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional Ubigia specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseUbigiaContext<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string address,
            [NotNull] string storage,
            [NotNull] string username,
            [NotNull] string password,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseUbigiaContext<GrpcTransport>(optionsBuilder, address, storage, username, password, ubigiaOptionsAction);

        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <typeparam name="TContext"> The type of context being configured. </typeparam>
        /// <typeparam name="TTransport"> The type of the transport to use. </typeparam>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="address">
        ///     The address of the Ubigia database. 
        /// </param>
        /// <param name="storage">
        ///     The storage that should be queried.
        /// </param>
        /// <param name="username">
        ///     The username with which to access the storage.
        /// </param>
        /// <param name="password">
        ///     The password with which to access the storage.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional Ubigia specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseUbigiaContext<TContext, TTransport>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string address,
            [NotNull] string storage,
            [NotNull] string username,
            [NotNull] string password,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TContext : DbContext
            where TTransport : ITransport
            => (DbContextOptionsBuilder<TContext>)UseUbigiaContext<TTransport>(optionsBuilder, address, storage, username, password, ubigiaOptionsAction);
        
        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="address">
        ///     The address of the Ubigia database. 
        /// </param>
        /// <param name="storage">
        ///     The storage that should be queried.
        /// </param>
        /// <param name="username">
        ///     The username with which to access the storage.
        /// </param>
        /// <param name="password">
        ///     The password with which to access the storage.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional Ubigia specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseUbigiaContext(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string address,
            [NotNull] string storage,
            [NotNull] string username,
            [NotNull] string password,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            => UseUbigiaContext<GrpcTransport>(optionsBuilder, address, storage, username, password, ubigiaOptionsAction);

        /// <summary>
        ///     Configures the context to connect to a Ubigia context.
        ///     passing a shared <see cref="UbigiaDatabaseRoot" /> on which to root the database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="address">
        ///     The address of the Ubigia database. 
        /// </param>
        /// <param name="storage">
        ///     The storage that should be queried.
        /// </param>
        /// <param name="username">
        ///     The username with which to access the storage.
        /// </param>
        /// <param name="password">
        ///     The password with which to access the storage.
        /// </param>
        /// <param name="ubigiaOptionsAction">An optional action to allow additional Ubigia specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseUbigiaContext<TTransport>(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string address,
            [NotNull] string storage,
            [NotNull] string username,
            [NotNull] string password,
            [CanBeNull] Action<UbigiaDbContextOptionsBuilder> ubigiaOptionsAction = null)
            where TTransport: ITransport
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotEmpty(address, nameof(address));
            Check.NotEmpty(storage, nameof(storage));
            Check.NotEmpty(username, nameof(username));
            Check.NotEmpty(password, nameof(password));

            var extension = optionsBuilder.Options.FindExtension<UbigiaOptionsExtension>()
                ?? new UbigiaOptionsExtension();

            extension = extension
                .WithAddress(address)
                .WithStorage(storage)
                .WithPassword(password)
                .WithUsername(username)
                .WithTransport<TTransport>();

            ConfigureWarnings(optionsBuilder);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ubigiaOptionsAction?.Invoke(new UbigiaDbContextOptionsBuilder(optionsBuilder));

            // Ubigia specifics.
            // We want to replace the DbSets with our own variation.
            // Reason is that not everything that the EF allows is a good match for accessing a Ubigia store.
#pragma warning disable EF1001
            optionsBuilder.ReplaceService<IDbSetSource, UbigiaDbSetSource>();
            optionsBuilder.ReplaceService<IDbSetFinder, UbigiaDbSetFinder>();
#pragma warning restore EF1001

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
