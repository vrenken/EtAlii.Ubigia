// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1128 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Metadata.Conventions
{
    using System.Linq.Expressions;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
    using Microsoft.EntityFrameworkCore.Query;

    /// <summary>
    ///     Convention that converts accesses of <see cref="DbSet{TEntity}" /> inside query filters and defining queries into
    ///     <see cref="QueryRootExpression" />.
    ///     This makes them consistent with how DbSet accesses in the actual queries are represented, which allows for easier processing in the
    ///     query pipeline.
    /// </summary>
    public class DefiningQueryRewritingConvention : QueryFilterRewritingConvention
    {
        /// <summary>
        ///     Creates a new instance of <see cref="QueryFilterRewritingConvention" />.
        /// </summary>
        /// <param name="dependencies"> Parameter object containing dependencies for this convention. </param>
        public DefiningQueryRewritingConvention([NotNull] ProviderConventionSetBuilderDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <inheritdoc />
        public override void ProcessModelFinalizing(
            IConventionModelBuilder modelBuilder,
            IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
            {
                var definingQuery = entityType.GetUbigiaQuery();
                if (definingQuery != null)
                {
                    entityType.SetUbigiaQuery((LambdaExpression)DbSetAccessRewriter.Rewrite(modelBuilder.Metadata, definingQuery));
                }
            }
        }
    }
}
