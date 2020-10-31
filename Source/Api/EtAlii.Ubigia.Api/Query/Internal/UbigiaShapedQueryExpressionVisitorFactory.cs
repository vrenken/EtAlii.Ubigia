// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Query;

namespace EtAlii.Ubigia.Api.Query.Internal
{
    public class UbigiaShapedQueryCompilingExpressionVisitorFactory : IShapedQueryCompilingExpressionVisitorFactory
    {
        private readonly ShapedQueryCompilingExpressionVisitorDependencies _dependencies;

        public UbigiaShapedQueryCompilingExpressionVisitorFactory(ShapedQueryCompilingExpressionVisitorDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual ShapedQueryCompilingExpressionVisitor Create(QueryCompilationContext queryCompilationContext)
            => new UbigiaShapedQueryCompilingExpressionVisitor(_dependencies, queryCompilationContext);
    }
}
