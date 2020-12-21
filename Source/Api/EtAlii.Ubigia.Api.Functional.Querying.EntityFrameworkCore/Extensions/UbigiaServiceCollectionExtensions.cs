// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Diagnostics.Internal;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Internal;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Metadata.Conventions;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Query.Internal;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage.Internal;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.ValueGeneration.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Utilities;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    /// <summary>
    ///     Ubigia specific extension methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class UbigiaServiceCollectionExtensions
    {
        /// <summary>
        ///     <para>
        ///         Adds the services required by the Ubigia database provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />.
        ///     </para>
        ///     <para>
        ///         Calling this method is no longer necessary when building most applications, including those that
        ///         use dependency injection in ASP.NET or elsewhere.
        ///         It is only needed when building the internal service provider for use with
        ///         the <see cref="DbContextOptionsBuilder.UseInternalServiceProvider" /> method.
        ///         This is not recommend other than for some advanced scenarios.
        ///     </para>
        /// </summary>
        /// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddEntityFrameworkUbigiaDatabase([NotNull] this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var builder = new EntityFrameworkServicesBuilder(serviceCollection)
                .TryAdd<LoggingDefinitions, UbigiaLoggingDefinitions>()
                .TryAdd<IDatabaseProvider, DatabaseProvider<UbigiaOptionsExtension>>()
                .TryAdd<IValueGeneratorSelector, UbigiaValueGeneratorSelector>()
                .TryAdd<IDatabase>(p => p.GetService<IUbigiaDatabase>())
                .TryAdd<IDbContextTransactionManager, UbigiaTransactionManager>()
                .TryAdd<IDatabaseCreator, UbigiaDatabaseCreator>()
                .TryAdd<IQueryContextFactory, UbigiaQueryContextFactory>()
                .TryAdd<IProviderConventionSetBuilder, UbigiaConventionSetBuilder>()
                .TryAdd<IModelValidator, UbigiaModelValidator>()
                .TryAdd<ITypeMappingSource, UbigiaTypeMappingSource>()
                .TryAdd<IShapedQueryCompilingExpressionVisitorFactory, UbigiaShapedQueryCompilingExpressionVisitorFactory>()
                .TryAdd<IQueryableMethodTranslatingExpressionVisitorFactory, UbigiaQueryableMethodTranslatingExpressionVisitorFactory>()
                .TryAdd<ISingletonOptions, IUbigiaSingletonOptions>(p => p.GetService<IUbigiaSingletonOptions>())
                .TryAddProviderSpecificServices(
                    b => b
                        .TryAddSingleton<IUbigiaSingletonOptions, UbigiaSingletonOptions>()
                        .TryAddSingleton<IUbigiaStoreCache, UbigiaStoreCache>()
                        .TryAddSingleton<IUbigiaTableFactory, UbigiaTableFactory>()
                        .TryAddScoped<IUbigiaDatabase, UbigiaDatabase>());

            builder.TryAddCoreServices();

            return serviceCollection;
        }
    }
}
