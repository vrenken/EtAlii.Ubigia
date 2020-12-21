// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S3358 // This code will change. remove this pragma afterwards.
#pragma warning disable S1144 // This code will change. remove this pragma afterwards.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Query.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Utilities;
    using ExpressionExtensions = Microsoft.EntityFrameworkCore.Infrastructure.ExpressionExtensions;

    public partial class UbigiaShapedQueryCompilingExpressionVisitor
    {
        private sealed class UbigiaProjectionBindingRemovingExpressionVisitor : ExpressionVisitor
        {
            private readonly IDictionary<ParameterExpression, (IDictionary<IProperty, int> IndexMap, ParameterExpression valueBuffer)>
                _materializationContextBindings
                    = new Dictionary<ParameterExpression, (IDictionary<IProperty, int> IndexMap, ParameterExpression valueBuffer)>();

            protected override Expression VisitBinary(BinaryExpression node)
            {
                Check.NotNull(node, nameof(node));

                if (node.NodeType == ExpressionType.Assign
                    && node.Left is ParameterExpression parameterExpression
                    && parameterExpression.Type == typeof(MaterializationContext))
                {
                    var newExpression = (NewExpression)node.Right;

                    var projectionBindingExpression = (ProjectionBindingExpression)newExpression.Arguments[0];
                    var queryExpression = (UbigiaQueryExpression)projectionBindingExpression.QueryExpression;

                    _materializationContextBindings[parameterExpression]
                        = ((IDictionary<IProperty, int>)GetProjectionIndex(queryExpression, projectionBindingExpression),
                            ((UbigiaQueryExpression)projectionBindingExpression.QueryExpression).CurrentParameter);

                    var updatedExpression = Expression.New(
                        newExpression.Constructor,
                        Expression.Constant(ValueBuffer.Empty),
                        newExpression.Arguments[1]);

                    return Expression.MakeBinary(ExpressionType.Assign, node.Left, updatedExpression);
                }

                if (node.NodeType == ExpressionType.Assign
                    && node.Left is MemberExpression memberExpression
                    && memberExpression.Member is FieldInfo fieldInfo
                    && fieldInfo.IsInitOnly)
                {
                    return memberExpression.Assign(Visit(node.Right));
                }

                return base.VisitBinary(node);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                Check.NotNull(node, nameof(node));

                if (node.Method.IsGenericMethod
                    && node.Method.GetGenericMethodDefinition() == ExpressionExtensions.ValueBufferTryReadValueMethod)
                {
                    var property = (IProperty)((ConstantExpression)node.Arguments[2]).Value;
                    var (indexMap, valueBuffer) =
                        _materializationContextBindings[
                            (ParameterExpression)((MethodCallExpression)node.Arguments[0]).Object];

                    Check.DebugAssert(
                        property != null || node.Type.IsNullableType(), "Must read nullable value without property");

                    return Expression.Call(
                        node.Method,
                        valueBuffer,
                        Expression.Constant(indexMap[property]),
                        node.Arguments[2]);
                }

                return base.VisitMethodCall(node);
            }

            protected override Expression VisitExtension(Expression node)
            {
                Check.NotNull(node, nameof(node));

                if (node is ProjectionBindingExpression projectionBindingExpression)
                {
                    var queryExpression = (UbigiaQueryExpression)projectionBindingExpression.QueryExpression;
                    var projectionIndex = (int)GetProjectionIndex(queryExpression, projectionBindingExpression);
                    var valueBuffer = queryExpression.CurrentParameter;
                    var property = InferPropertyFromInner(queryExpression.Projection[projectionIndex]);

                    Check.DebugAssert(
                        property != null
                        || projectionBindingExpression.Type.IsNullableType()
                        || projectionBindingExpression.Type == typeof(ValueBuffer), "Must read nullable value without property");

                    return valueBuffer.CreateValueBufferReadValueExpression(projectionBindingExpression.Type, projectionIndex, property);
                }

                return base.VisitExtension(node);
            }

            private IPropertyBase InferPropertyFromInner(Expression expression)
            {
                if (expression is MethodCallExpression methodCallExpression
                    && methodCallExpression.Method.IsGenericMethod
                    && methodCallExpression.Method.GetGenericMethodDefinition() == ExpressionExtensions.ValueBufferTryReadValueMethod)
                {
                    return (IPropertyBase)((ConstantExpression)methodCallExpression.Arguments[2]).Value;
                }

                return null;
            }

            private object GetProjectionIndex(
                UbigiaQueryExpression queryExpression,
                ProjectionBindingExpression projectionBindingExpression)
            {
                return projectionBindingExpression.ProjectionMember != null
                    ? ((ConstantExpression)queryExpression.GetMappedProjection(projectionBindingExpression.ProjectionMember)).Value
                    : (projectionBindingExpression.Index != null
                        ? (object)projectionBindingExpression.Index
                        : projectionBindingExpression.IndexMap);
            }
        }
    }
}
