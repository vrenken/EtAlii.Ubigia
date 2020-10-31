// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Query.Internal
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;

    public partial class UbigiaShapedQueryCompilingExpressionVisitor : ShapedQueryCompilingExpressionVisitor
    {
        private readonly Type _contextType;
        private readonly IDiagnosticsLogger<DbLoggerCategory.Query> _logger;

        public UbigiaShapedQueryCompilingExpressionVisitor(
            ShapedQueryCompilingExpressionVisitorDependencies dependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, queryCompilationContext)
        {
            _contextType = queryCompilationContext.ContextType;
            _logger = AppContext.TryGetSwitch("Microsoft.EntityFrameworkCore.Issue21016", out var isEnabled) && isEnabled
                ? queryCompilationContext.Logger
                : null;
        }

        protected override Expression VisitExtension(Expression extensionExpression)
        {
            switch (extensionExpression)
            {
                case UbigiaQueryExpression ubigiaQueryExpression:
                    ubigiaQueryExpression.ApplyProjection();
                    return Visit(ubigiaQueryExpression.ServerQueryExpression);

                case UbigiaTableExpression ubigiaTableExpression:
                    return Expression.Call(
                        TableMethodInfo,
                        QueryCompilationContext.QueryContextParameter,
                        Expression.Constant(ubigiaTableExpression.EntityType));
            }

            return base.VisitExtension(extensionExpression);
        }

        protected override Expression VisitShapedQueryExpression(ShapedQueryExpression shapedQueryExpression)
        {
            var ubigiaQueryExpression = (UbigiaQueryExpression)shapedQueryExpression.QueryExpression;

            var shaper = new ShaperExpressionProcessingExpressionVisitor(
                    ubigiaQueryExpression, ubigiaQueryExpression.CurrentParameter)
                .Inject(shapedQueryExpression.ShaperExpression);

            shaper = InjectEntityMaterializers(shaper);

            var innerEnumerable = Visit(ubigiaQueryExpression);

            shaper = new UbigiaProjectionBindingRemovingExpressionVisitor().Visit(shaper);

            shaper = new CustomShaperCompilingExpressionVisitor(IsTracking).Visit(shaper);

            var shaperLambda = (LambdaExpression)shaper;

            return Expression.New(
                typeof(QueryingEnumerable<>).MakeGenericType(shaperLambda.ReturnType).GetConstructors()[0],
                QueryCompilationContext.QueryContextParameter,
                innerEnumerable,
                Expression.Constant(shaperLambda.Compile()),
                Expression.Constant(_contextType),
                Expression.Constant(_logger, typeof(IDiagnosticsLogger<DbLoggerCategory.Query>)));
        }

        private static readonly MethodInfo TableMethodInfo = typeof(UbigiaShapedQueryCompilingExpressionVisitor).GetTypeInfo().GetDeclaredMethod(nameof(Table));

        private static IEnumerable<ValueBuffer> Table(
            QueryContext queryContext,
            IEntityType entityType)
        {
            return ((UbigiaQueryContext)queryContext).Store
                .GetTables(entityType)
                .SelectMany(t => t.Rows.Select(vs => new ValueBuffer(vs)));
        }
    }
}
