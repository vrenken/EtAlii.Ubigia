// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Utilities;

    /// <summary>
    ///     Extension methods for <see cref="EntityTypeBuilder" /> for the Ubigia provider.
    /// </summary>
    public static class UbigiaEntityTypeBuilderExtensions
    {
        /// <summary>
        ///     Configures a query used to provide data for an entity type.
        /// </summary>
        /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
        /// <param name="query"> The query that will provide the underlying data for the entity type. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static EntityTypeBuilder<TEntity> ToUbigiaQuery<TEntity>(
            [NotNull] this EntityTypeBuilder<TEntity> entityTypeBuilder,
            [NotNull] Expression<Func<IQueryable<TEntity>>> query)
            where TEntity : class
        {
            Check.NotNull(query, nameof(query));

            entityTypeBuilder.Metadata.SetUbigiaQuery(query);

            return entityTypeBuilder;
        }

        /// <summary>
        ///     Configures a query used to provide data for an entity type.
        /// </summary>
        /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
        /// <param name="query"> The query that will provide the underlying data for the entity type. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns>
        ///     The same builder instance if the query was set, <see langword="null" /> otherwise.
        /// </returns>
        public static IConventionEntityTypeBuilder ToUbigiaQuery(
            [NotNull] this IConventionEntityTypeBuilder entityTypeBuilder,
            [CanBeNull] LambdaExpression query,
            bool fromDataAnnotation = false)
        {
            if (CanSetUbigiaQuery(entityTypeBuilder, query, fromDataAnnotation))
            {
                entityTypeBuilder.Metadata.SetUbigiaQuery(query, fromDataAnnotation);

                return entityTypeBuilder;
            }

            return null;
        }

        /// <summary>
        ///     Returns a value indicating whether the given Ubigia query can be set from the current configuration source.
        /// </summary>
        /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
        /// <param name="query"> The query that will provide the underlying data for the keyless entity type. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns> <see langword="true" /> if the given Ubigia query can be set. </returns>
        public static bool CanSetUbigiaQuery(
            [NotNull] this IConventionEntityTypeBuilder entityTypeBuilder,
            [CanBeNull] LambdaExpression query,
            bool fromDataAnnotation = false)
#pragma warning disable EF1001 // Internal EF Core API usage.
#pragma warning disable CS0612 // Type or member is obsolete
            => entityTypeBuilder.CanSetAnnotation(CoreAnnotationNames.DefiningQuery, query, fromDataAnnotation);
#pragma warning restore CS0612 // Type or member is obsolete
#pragma warning restore EF1001 // Internal EF Core API usage.
    }
}
