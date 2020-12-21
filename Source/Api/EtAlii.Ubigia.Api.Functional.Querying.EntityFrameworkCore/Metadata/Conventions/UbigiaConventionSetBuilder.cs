// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Metadata.Conventions
{
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     <para>
    ///         A builder for building conventions for th Ubigia provider.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Scoped" /> and multiple registrations
    ///         are allowed. This means that each <see cref="DbContext" /> instance will use its own
    ///         set of instances of this service.
    ///         The implementations may depend on other services registered with any lifetime.
    ///         The implementations do not need to be thread-safe.
    ///     </para>
    /// </summary>
    public class UbigiaConventionSetBuilder : ProviderConventionSetBuilder
    {
        /// <summary>
        ///     Creates a new <see cref="UbigiaConventionSetBuilder" /> instance.
        /// </summary>
        /// <param name="dependencies"> The core dependencies for this service. </param>
        public UbigiaConventionSetBuilder(
            [NotNull] ProviderConventionSetBuilderDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <inheritdoc />
        public override ConventionSet CreateConventionSet()
        {
            var conventionSet = base.CreateConventionSet();

            conventionSet.ModelFinalizingConventions.Add(new DefiningQueryRewritingConvention(Dependencies));

            return conventionSet;
        }
        //
        // /// <summary>
        // ///     <para>
        // ///         Call this method to build a <see cref="ConventionSet" /> for the Ubigia provider when using
        // ///         the <see cref="ModelBuilder" /> outside of <see cref="DbContext.OnModelCreating" />.
        // ///     </para>
        // ///     <para>
        // ///         Note that it is unusual to use this method.
        // ///         Consider using <see cref="DbContext" /> in the normal way instead.
        // ///     </para>
        // /// </summary>
        // /// <returns> The convention set. </returns>
        // public static ConventionSet Build()
        // [
        //     using var serviceScope = CreateServiceScope()
        //     using var context = serviceScope.ServiceProvider.GetService<DbContext>()
        //     return ConventionSet.CreateConventionSet(context)
        // ]
        //
        // /// <summary>
        // ///     <para>
        // ///         Call this method to build a <see cref="ModelBuilder" /> for SQLite outside of <see cref="DbContext.OnModelCreating" />.
        // ///     </para>
        // ///     <para>
        // ///         Note that it is unusual to use this method.
        // ///         Consider using <see cref="DbContext" /> in the normal way instead.
        // ///     </para>
        // /// </summary>
        // /// <returns> The convention set. </returns>
        // public static ModelBuilder CreateModelBuilder()
        // [
        //     using var serviceScope = CreateServiceScope()
        //     using var context = serviceScope.ServiceProvider.GetService<DbContext>()
        //     return new ModelBuilder(ConventionSet.CreateConventionSet(context), context.GetService<ModelDependencies>())
        // ]
        //
        // private static IServiceScope CreateServiceScope()
        // [
        //     var serviceProvider = new ServiceCollection()
        //         .AddEntityFrameworkUbigiaDatabase()
        //         .AddDbContext<DbContext>(
        //             (p, o) =>
        //                 o.UseUbigiaContext(Guid.NewGuid().ToString())
        //                     .UseInternalServiceProvider(p))
        //         .BuildServiceProvider()
        //
        //     return serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()
        // ]
    }
}
