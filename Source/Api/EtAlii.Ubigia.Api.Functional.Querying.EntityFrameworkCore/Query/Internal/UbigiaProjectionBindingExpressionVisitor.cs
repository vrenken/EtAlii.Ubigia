// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Query.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Utilities;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaProjectionBindingExpressionVisitor : ExpressionVisitor
    {
        private readonly UbigiaQueryableMethodTranslatingExpressionVisitor _queryableMethodTranslatingExpressionVisitor;
        private readonly UbigiaExpressionTranslatingExpressionVisitor _expressionTranslatingExpressionVisitor;

        private UbigiaQueryExpression _queryExpression;
        private bool _clientEval;

        private readonly IDictionary<ProjectionMember, Expression> _projectionMapping
            = new Dictionary<ProjectionMember, Expression>();

        private readonly Stack<ProjectionMember> _projectionMembers = new();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaProjectionBindingExpressionVisitor(
            [NotNull] UbigiaQueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor,
            [NotNull] UbigiaExpressionTranslatingExpressionVisitor expressionTranslatingExpressionVisitor)
        {
            _queryableMethodTranslatingExpressionVisitor = queryableMethodTranslatingExpressionVisitor;
            _expressionTranslatingExpressionVisitor = expressionTranslatingExpressionVisitor;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual Expression Translate([NotNull] UbigiaQueryExpression queryExpression, [NotNull] Expression expression)
        {
            _queryExpression = queryExpression;
            _clientEval = false;

            _projectionMembers.Push(new ProjectionMember());

            var expandedExpression = _queryableMethodTranslatingExpressionVisitor.ExpandWeakEntities(_queryExpression, expression);
            var result = Visit(expandedExpression);

            if (result == null)
            {
                _clientEval = true;

                expandedExpression = _queryableMethodTranslatingExpressionVisitor.ExpandWeakEntities(_queryExpression, expression);
                result = Visit(expandedExpression);

                _projectionMapping.Clear();
            }

            _queryExpression.ReplaceProjectionMapping(_projectionMapping);
            _queryExpression = null;
            _projectionMapping.Clear();
            _projectionMembers.Clear();

            result = MatchTypes(result, expression.Type);

            return result;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return null;
            }

            if (!(node is NewExpression
                || node is MemberInitExpression
                || node is EntityShaperExpression
                || node is IncludeExpression))
            {
                // This skips the group parameter from GroupJoin
                if (node is ParameterExpression parameter
                    && parameter.Type.IsGenericType
                    && parameter.Type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return parameter;
                }

                if (_clientEval)
                {
                    switch (node)
                    {
                        case ConstantExpression _:
                            return node;

                        case MaterializeCollectionNavigationExpression materializeCollectionNavigationExpression:
                            return AddCollectionProjection(
                                _queryableMethodTranslatingExpressionVisitor.TranslateSubquery(
                                    materializeCollectionNavigationExpression.Subquery),
                                materializeCollectionNavigationExpression.Navigation,
                                null);

                        case MethodCallExpression methodCallExpression:
                        {
                            if (methodCallExpression.Method.IsGenericMethod
                                && methodCallExpression.Method.DeclaringType == typeof(Enumerable)
                                && methodCallExpression.Method.Name == nameof(Enumerable.ToList))
                            {
                                var subqueryTranslation = _queryableMethodTranslatingExpressionVisitor.TranslateSubquery(
                                    methodCallExpression.Arguments[0]);

                                if (subqueryTranslation != null)
                                {
                                    return AddCollectionProjection(
                                        subqueryTranslation,
                                        null,
                                        methodCallExpression.Method.GetGenericArguments()[0]);
                                }
                            }
                            else
                            {
                                var subquery = _queryableMethodTranslatingExpressionVisitor.TranslateSubquery(methodCallExpression);
                                if (subquery != null)
                                {
                                    if (subquery.ResultCardinality == ResultCardinality.Enumerable)
                                    {
                                        return AddCollectionProjection(subquery, null, subquery.ShaperExpression.Type);
                                    }

                                    return new SingleResultShaperExpression(
                                        new ProjectionBindingExpression(
                                            _queryExpression,
                                            _queryExpression.AddSubqueryProjection(subquery, out var innerShaper),
                                            typeof(ValueBuffer)),
                                        innerShaper,
                                        subquery.ShaperExpression.Type);
                                }
                            }

                            break;
                        }
                    }

                    var translation = _expressionTranslatingExpressionVisitor.Translate(node);
                    return translation == null
                        ? base.Visit(node)
                        : new ProjectionBindingExpression(
                            _queryExpression, _queryExpression.AddToProjection(translation), node.Type.MakeNullable());
                }
                else
                {
                    var translation = _expressionTranslatingExpressionVisitor.Translate(node);
                    if (translation == null)
                    {
                        return null;
                    }

                    _projectionMapping[_projectionMembers.Peek()] = translation;

                    return new ProjectionBindingExpression(_queryExpression, _projectionMembers.Peek(), node.Type.MakeNullable());
                }
            }

            return base.Visit(node);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = MatchTypes(Visit(node.Left), node.Left.Type);
            var right = MatchTypes(Visit(node.Right), node.Right.Type);

            return node.Update(left, VisitAndConvert(node.Conversion, "VisitBinary"), right);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            var test = Visit(node.Test);
            var ifTrue = Visit(node.IfTrue);
            var ifFalse = Visit(node.IfFalse);

            if (test.Type == typeof(bool?))
            {
                test = Expression.Equal(test, Expression.Constant(true, typeof(bool?)));
            }

            return node.Update(test, ifTrue, ifFalse);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitExtension(Expression node)
        {
            Check.NotNull(node, nameof(node));

            if (node is EntityShaperExpression entityShaperExpression)
            {
                EntityProjectionExpression entityProjectionExpression;
                if (entityShaperExpression.ValueBufferExpression is ProjectionBindingExpression projectionBindingExpression)
                {
                    entityProjectionExpression = (EntityProjectionExpression)((UbigiaQueryExpression)projectionBindingExpression.QueryExpression)
                        .GetMappedProjection(projectionBindingExpression.ProjectionMember);
                }
                else
                {
                    entityProjectionExpression = (EntityProjectionExpression)entityShaperExpression.ValueBufferExpression;
                }

                if (_clientEval)
                {
                    return entityShaperExpression.Update(
                        new ProjectionBindingExpression(_queryExpression, _queryExpression.AddToProjection(entityProjectionExpression)));
                }

                _projectionMapping[_projectionMembers.Peek()] = entityProjectionExpression;

                return entityShaperExpression.Update(
                    new ProjectionBindingExpression(_queryExpression, _projectionMembers.Peek(), typeof(ValueBuffer)));
            }

            if (node is IncludeExpression includeExpression)
            {
                return _clientEval
                    ? base.VisitExtension(includeExpression)
                    : null;
            }

            throw new InvalidOperationException(CoreStrings.QueryFailed(node.Print(), GetType().Name));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ElementInit VisitElementInit(ElementInit node)
            => node.Update(node.Arguments.Select(e => MatchTypes(Visit(e), e.Type)));

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitMember(MemberExpression node)
        {
            var expression = Visit(node.Expression);
            Expression updatedMemberExpression = node.Update(
                expression != null ? MatchTypes(expression, node.Expression.Type) : expression);

            if (expression?.Type.IsNullableValueType() == true)
            {
                var nullableReturnType = node.Type.MakeNullable();
                if (!node.Type.IsNullableType())
                {
                    updatedMemberExpression = Expression.Convert(updatedMemberExpression, nullableReturnType);
                }

                updatedMemberExpression = Expression.Condition(
                    Expression.Equal(expression, Expression.Default(expression.Type)),
                    Expression.Constant(null, nullableReturnType),
                    updatedMemberExpression);
            }

            return updatedMemberExpression;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            var expression = node.Expression;
            Expression visitedExpression;
            if (_clientEval)
            {
                visitedExpression = Visit(node.Expression);
            }
            else
            {
                var projectionMember = _projectionMembers.Peek().Append(node.Member);
                _projectionMembers.Push(projectionMember);

                visitedExpression = Visit(node.Expression);
                if (visitedExpression == null)
                {
                    return null;
                }

                _projectionMembers.Pop();
            }

            visitedExpression = MatchTypes(visitedExpression, expression.Type);

            return node.Update(visitedExpression);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            Check.NotNull(node, nameof(node));

            var newExpression = Visit(node.NewExpression);
            if (newExpression == null)
            {
                return null;
            }

            var newBindings = new MemberBinding[node.Bindings.Count];
            for (var i = 0; i < newBindings.Length; i++)
            {
                if (node.Bindings[i].BindingType != MemberBindingType.Assignment)
                {
                    return null;
                }

                newBindings[i] = VisitMemberBinding(node.Bindings[i]);
                if (newBindings[i] == null)
                {
                    return null;
                }
            }

            return node.Update((NewExpression)newExpression, newBindings);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var @object = Visit(node.Object);
            var arguments = new Expression[node.Arguments.Count];
            for (var i = 0; i < node.Arguments.Count; i++)
            {
                var argument = node.Arguments[i];
                arguments[i] = MatchTypes(Visit(argument), argument.Type);
            }

            Expression updatedMethodCallExpression = node.Update(
                @object != null ? MatchTypes(@object, node.Object.Type) : @object,
                arguments);

            if (@object?.Type.IsNullableType() == true
                && !node.Object.Type.IsNullableType())
            {
                var nullableReturnType = node.Type.MakeNullable();
                if (!node.Type.IsNullableType())
                {
                    updatedMethodCallExpression = Expression.Convert(updatedMethodCallExpression, nullableReturnType);
                }

                return Expression.Condition(
                    Expression.Equal(@object, Expression.Default(@object.Type)),
                    Expression.Constant(null, nullableReturnType),
                    updatedMethodCallExpression);
            }

            return updatedMethodCallExpression;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitNew(NewExpression node)
        {
            Check.NotNull(node, nameof(node));

            if (node.Arguments.Count == 0)
            {
                return node;
            }

            if (!_clientEval
                && node.Members == null)
            {
                return null;
            }

            var newArguments = new Expression[node.Arguments.Count];
            for (var i = 0; i < newArguments.Length; i++)
            {
                var argument = node.Arguments[i];
                Expression visitedArgument;
                if (_clientEval)
                {
                    visitedArgument = Visit(argument);
                }
                else
                {
                    var projectionMember = _projectionMembers.Peek().Append(node.Members[i]);
                    _projectionMembers.Push(projectionMember);
                    visitedArgument = Visit(argument);
                    if (visitedArgument == null)
                    {
                        return null;
                    }

                    _projectionMembers.Pop();
                }

                newArguments[i] = MatchTypes(visitedArgument, argument.Type);
            }

            return node.Update(newArguments);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitNewArray(NewArrayExpression node)
            => node.Update(node.Expressions.Select(e => MatchTypes(Visit(e), e.Type)));

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var operand = Visit(node.Operand);

            return (node.NodeType == ExpressionType.Convert
                    || node.NodeType == ExpressionType.ConvertChecked)
                && node.Type == operand.Type
                    ? operand
                    : node.Update(MatchTypes(operand, node.Operand.Type));
        }

        private CollectionShaperExpression AddCollectionProjection(
            ShapedQueryExpression subquery,
            INavigationBase navigation,
            Type elementType)
            => new(
                new ProjectionBindingExpression(
                    _queryExpression,
                    _queryExpression.AddSubqueryProjection(
                        subquery,
                        out var innerShaper),
                    typeof(IEnumerable<ValueBuffer>)),
                innerShaper,
                navigation,
                elementType);

        private static Expression MatchTypes(Expression expression, Type targetType)
        {
            if (targetType != expression.Type
                && targetType.TryGetElementType(typeof(IQueryable<>)) == null)
            {
                Check.DebugAssert(targetType.MakeNullable() == expression.Type, "Not a nullable to non-nullable conversion");

                expression = Expression.Convert(expression, targetType);
            }

            return expression;
        }
    }
}
