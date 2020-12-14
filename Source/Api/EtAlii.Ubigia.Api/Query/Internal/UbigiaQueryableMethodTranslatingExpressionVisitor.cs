// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// This code will change. Remove these pragma afterwards.
#pragma warning disable S4136
#pragma warning disable S3358
#pragma warning disable S1144
#pragma warning disable S4144 // Methods should not have identical implementations.
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high

namespace EtAlii.Ubigia.Api.Query.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
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
    public sealed class UbigiaQueryableMethodTranslatingExpressionVisitor : QueryableMethodTranslatingExpressionVisitor
    {
        private readonly UbigiaExpressionTranslatingExpressionVisitor _expressionTranslator;
        private readonly WeakEntityExpandingExpressionVisitor _weakEntityExpandingExpressionVisitor;
        private readonly UbigiaProjectionBindingExpressionVisitor _projectionBindingExpressionVisitor;
        private readonly IModel _model;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaQueryableMethodTranslatingExpressionVisitor(
            [NotNull] QueryableMethodTranslatingExpressionVisitorDependencies dependencies,
            [NotNull] QueryCompilationContext queryCompilationContext)
            : base(dependencies, queryCompilationContext, subquery: false)
        {
            _expressionTranslator = new UbigiaExpressionTranslatingExpressionVisitor(queryCompilationContext, this);
            _weakEntityExpandingExpressionVisitor = new WeakEntityExpandingExpressionVisitor(_expressionTranslator);
            _projectionBindingExpressionVisitor = new UbigiaProjectionBindingExpressionVisitor(this, _expressionTranslator);
            _model = queryCompilationContext.Model;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        private UbigiaQueryableMethodTranslatingExpressionVisitor(
            [NotNull] UbigiaQueryableMethodTranslatingExpressionVisitor parentVisitor)
            : base(parentVisitor.Dependencies, parentVisitor.QueryCompilationContext, subquery: true)
        {
            _expressionTranslator = new UbigiaExpressionTranslatingExpressionVisitor(QueryCompilationContext, parentVisitor);
            _weakEntityExpandingExpressionVisitor = new WeakEntityExpandingExpressionVisitor(_expressionTranslator);
            _projectionBindingExpressionVisitor = new UbigiaProjectionBindingExpressionVisitor(this, _expressionTranslator);
            _model = parentVisitor._model;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override QueryableMethodTranslatingExpressionVisitor CreateSubqueryVisitor()
            => new UbigiaQueryableMethodTranslatingExpressionVisitor(this);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitMethodCall(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method.IsGenericMethod
                && methodCallExpression.Arguments.Count == 1
                && methodCallExpression.Arguments[0].Type.TryGetSequenceType() != null
                && string.Equals(methodCallExpression.Method.Name, "AsSplitQuery", StringComparison.Ordinal))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                return Visit(methodCallExpression.Arguments[0]);
            }

            return base.VisitMethodCall(methodCallExpression);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        [Obsolete("Use overload which takes IEntityType.")]
        protected override ShapedQueryExpression CreateShapedQueryExpression(Type elementType)
        {
            Check.NotNull(elementType, nameof(elementType));

            return CreateShapedQueryExpression(_model.FindEntityType(elementType));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression CreateShapedQueryExpression(IEntityType entityType)
            => CreateShapedQueryExpressionStatic(entityType);

        private static ShapedQueryExpression CreateShapedQueryExpressionStatic(IEntityType entityType)
        {
            var queryExpression = new UbigiaQueryExpression(entityType);

            return new ShapedQueryExpression(
                queryExpression,
                new EntityShaperExpression(
                    entityType,
                    new ProjectionBindingExpression(
                        queryExpression,
                        new ProjectionMember(),
                        typeof(ValueBuffer)),
                    false));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateAll(ShapedQueryExpression source, LambdaExpression predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            predicate = Expression.Lambda(Expression.Not(predicate.Body), predicate.Parameters);
            source = TranslateWhere(source, predicate);
            if (source == null)
            {
                return null;
            }

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            if (source.ShaperExpression is GroupByShaperExpression)
            {
                ubigiaQueryExpression.ReplaceProjectionMapping(new Dictionary<ProjectionMember, Expression>());
                ubigiaQueryExpression.PushdownIntoSubquery();
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Not(
                    Expression.Call(
                        EnumerableMethods.AnyWithoutPredicate.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                        ubigiaQueryExpression.ServerQueryExpression)));

            return source.UpdateShaperExpression(Expression.Convert(ubigiaQueryExpression.GetSingleScalarProjection(), typeof(bool)));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateAny(ShapedQueryExpression source, LambdaExpression predicate)
        {
            if (predicate != null)
            {
                source = TranslateWhere(source, predicate);
                if (source == null)
                {
                    return null;
                }
            }

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            if (source.ShaperExpression is GroupByShaperExpression)
            {
                ubigiaQueryExpression.ReplaceProjectionMapping(new Dictionary<ProjectionMember, Expression>());
                ubigiaQueryExpression.PushdownIntoSubquery();
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.AnyWithoutPredicate.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression));

            return source.UpdateShaperExpression(Expression.Convert(ubigiaQueryExpression.GetSingleScalarProjection(), typeof(bool)));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateAverage(ShapedQueryExpression source, LambdaExpression selector, Type resultType)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(resultType, nameof(resultType));

            return TranslateScalarAggregate(source, selector, nameof(Enumerable.Average), resultType);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateCast(ShapedQueryExpression source, Type castType)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(castType, nameof(castType));

            return source.ShaperExpression.Type != castType
                ? source.UpdateShaperExpression(Expression.Convert(source.ShaperExpression, castType))
                : source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateConcat(ShapedQueryExpression source1, ShapedQueryExpression source2)
        {
            Check.NotNull(source1, nameof(source1));
            Check.NotNull(source2, nameof(source2));

            return TranslateSetOperation(EnumerableMethods.Concat, source1, source2);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateContains(ShapedQueryExpression source, Expression item)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(item, nameof(item));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;
            item = TranslateExpression(item, preserveType: true);
            if (item == null)
            {
                return null;
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.Contains.MakeGenericMethod(item.Type),
                    Expression.Call(
                        EnumerableMethods.Select.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type, item.Type),
                        ubigiaQueryExpression.ServerQueryExpression,
                        Expression.Lambda(
                            ubigiaQueryExpression.GetMappedProjection(new ProjectionMember()), ubigiaQueryExpression.CurrentParameter)),
                    item));

            return source.UpdateShaperExpression(Expression.Convert(ubigiaQueryExpression.GetSingleScalarProjection(), typeof(bool)));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateCount(ShapedQueryExpression source, LambdaExpression predicate)
        {
            Check.NotNull(source, nameof(source));

            if (predicate != null)
            {
                source = TranslateWhere(source, predicate);
                if (source == null)
                {
                    return null;
                }
            }

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            if (source.ShaperExpression is GroupByShaperExpression)
            {
                ubigiaQueryExpression.ReplaceProjectionMapping(new Dictionary<ProjectionMember, Expression>());
                ubigiaQueryExpression.PushdownIntoSubquery();
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.CountWithoutPredicate.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression));

            return source.UpdateShaperExpression(Expression.Convert(ubigiaQueryExpression.GetSingleScalarProjection(), typeof(int)));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateDefaultIfEmpty(ShapedQueryExpression source, Expression defaultValue)
        {
            Check.NotNull(source, nameof(source));

            if (defaultValue == null)
            {
                ((UbigiaQueryExpression)source.QueryExpression).ApplyDefaultIfEmpty();
                return source.UpdateShaperExpression(MarkShaperNullable(source.ShaperExpression));
            }

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateDistinct(ShapedQueryExpression source)
        {
            Check.NotNull(source, nameof(source));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            ubigiaQueryExpression.PushdownIntoSubquery();
            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.Distinct.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression));

            return source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateElementAtOrDefault(
            ShapedQueryExpression source,
            Expression index,
            bool returnDefault)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(index, nameof(index));

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateExcept(ShapedQueryExpression source1, ShapedQueryExpression source2)
        {
            Check.NotNull(source1, nameof(source1));
            Check.NotNull(source2, nameof(source2));

            return TranslateSetOperation(EnumerableMethods.Except, source1, source2);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateFirstOrDefault(
            ShapedQueryExpression source,
            LambdaExpression predicate,
            Type returnType,
            bool returnDefault)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(returnType, nameof(returnType));

            return TranslateSingleResultOperator(
                source,
                predicate,
                returnType,
                returnDefault
                    ? EnumerableMethods.FirstOrDefaultWithoutPredicate
                    : EnumerableMethods.FirstWithoutPredicate);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateGroupBy(
            ShapedQueryExpression source,
            LambdaExpression keySelector,
            LambdaExpression elementSelector,
            LambdaExpression resultSelector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            var remappedKeySelector = RemapLambdaBody(source, keySelector);

            var translatedKey = TranslateGroupingKey(remappedKeySelector);
            if (translatedKey != null)
            {
                if (elementSelector != null)
                {
                    source = TranslateSelect(source, elementSelector);
                }

                var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;
                var groupByShaper = ubigiaQueryExpression.ApplyGrouping(translatedKey, source.ShaperExpression);

                if (resultSelector == null)
                {
                    return source.UpdateShaperExpression(groupByShaper);
                }

                var original1 = resultSelector.Parameters[0];
                var original2 = resultSelector.Parameters[1];

                var newResultSelectorBody = new ReplacingExpressionVisitor(
                    new Expression[] { original1, original2 },
                    new[] { groupByShaper.KeySelector, groupByShaper }).Visit(resultSelector.Body);

                newResultSelectorBody = ExpandWeakEntities(ubigiaQueryExpression, newResultSelectorBody);
                var newShaper = _projectionBindingExpressionVisitor.Translate(ubigiaQueryExpression, newResultSelectorBody);
                ubigiaQueryExpression.PushdownIntoSubquery();

                return source.UpdateShaperExpression(newShaper);
            }

            return null;
        }

        private Expression TranslateGroupingKey(Expression expression)
        {
            switch (expression)
            {
                case NewExpression newExpression:
                    if (newExpression.Arguments.Count == 0)
                    {
                        return newExpression;
                    }

                    var newArguments = new Expression[newExpression.Arguments.Count];
                    for (var i = 0; i < newArguments.Length; i++)
                    {
                        newArguments[i] = TranslateGroupingKey(newExpression.Arguments[i]);
                        if (newArguments[i] == null)
                        {
                            return null;
                        }
                    }

                    return newExpression.Update(newArguments);

                case MemberInitExpression memberInitExpression:
                    var updatedNewExpression = (NewExpression)TranslateGroupingKey(memberInitExpression.NewExpression);
                    if (updatedNewExpression == null)
                    {
                        return null;
                    }

                    var newBindings = new MemberAssignment[memberInitExpression.Bindings.Count];
                    for (var i = 0; i < newBindings.Length; i++)
                    {
                        var memberAssignment = (MemberAssignment)memberInitExpression.Bindings[i];
                        var visitedExpression = TranslateGroupingKey(memberAssignment.Expression);
                        if (visitedExpression == null)
                        {
                            return null;
                        }

                        newBindings[i] = memberAssignment.Update(visitedExpression);
                    }

                    return memberInitExpression.Update(updatedNewExpression, newBindings);

                default:
                    var translation = TranslateExpression(expression);
                    if (translation == null)
                    {
                        return null;
                    }

                    return translation.Type == expression.Type
                        ? translation
                        : Expression.Convert(translation, expression.Type);
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateGroupJoin(
            ShapedQueryExpression outer,
            ShapedQueryExpression inner,
            LambdaExpression outerKeySelector,
            LambdaExpression innerKeySelector,
            LambdaExpression resultSelector)
        {
            Check.NotNull(outer, nameof(outer));
            Check.NotNull(inner, nameof(inner));
            Check.NotNull(outerKeySelector, nameof(outerKeySelector));
            Check.NotNull(innerKeySelector, nameof(innerKeySelector));
            Check.NotNull(resultSelector, nameof(resultSelector));

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateIntersect(ShapedQueryExpression source1, ShapedQueryExpression source2)
        {
            Check.NotNull(source1, nameof(source1));
            Check.NotNull(source2, nameof(source2));

            return TranslateSetOperation(EnumerableMethods.Intersect, source1, source2);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateJoin(
            ShapedQueryExpression outer,
            ShapedQueryExpression inner,
            LambdaExpression outerKeySelector,
            LambdaExpression innerKeySelector,
            LambdaExpression resultSelector)
        {
            Check.NotNull(outer, nameof(outer));
            Check.NotNull(inner, nameof(inner));
            Check.NotNull(resultSelector, nameof(resultSelector));

            (outerKeySelector, innerKeySelector) = ProcessJoinKeySelector(outer, inner, outerKeySelector, innerKeySelector);

            if (outerKeySelector == null
                || innerKeySelector == null)
            {
                return null;
            }

            var transparentIdentifierType = TransparentIdentifierFactory.Create(
                resultSelector.Parameters[0].Type,
                resultSelector.Parameters[1].Type);

            ((UbigiaQueryExpression)outer.QueryExpression).AddInnerJoin(
                (UbigiaQueryExpression)inner.QueryExpression,
                outerKeySelector,
                innerKeySelector,
                transparentIdentifierType);

#pragma warning disable CS0618 // Type or member is obsolete See issue#21200
            return TranslateResultSelectorForJoin(
                outer,
                resultSelector,
                inner.ShaperExpression,
                transparentIdentifierType);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private (LambdaExpression OuterKeySelector, LambdaExpression InnerKeySelector) ProcessJoinKeySelector(
            ShapedQueryExpression outer,
            ShapedQueryExpression inner,
            LambdaExpression outerKeySelector,
            LambdaExpression innerKeySelector)
        {
            var left = RemapLambdaBody(outer, outerKeySelector);
            var right = RemapLambdaBody(inner, innerKeySelector);

            var joinCondition = TranslateExpression(Expression.Equal(left, right));

            var (outerKeyBody, innerKeyBody) = DecomposeJoinCondition(joinCondition);

            if (outerKeyBody == null
                || innerKeyBody == null)
            {
                return (null, null);
            }

            outerKeySelector = Expression.Lambda(outerKeyBody, ((UbigiaQueryExpression)outer.QueryExpression).CurrentParameter);
            innerKeySelector = Expression.Lambda(innerKeyBody, ((UbigiaQueryExpression)inner.QueryExpression).CurrentParameter);

            return AlignKeySelectorTypes(outerKeySelector, innerKeySelector);
        }

        private static (Expression, Expression) DecomposeJoinCondition(Expression joinCondition)
        {
            var leftExpressions = new List<Expression>();
            var rightExpressions = new List<Expression>();

            return ProcessJoinCondition(joinCondition, leftExpressions, rightExpressions)
                ? leftExpressions.Count == 1
                    ? (leftExpressions[0], rightExpressions[0])
                    : (CreateAnonymousObject(leftExpressions), CreateAnonymousObject(rightExpressions))
                : (null, null);

            // Ubigia joins need to use AnonymousObject to perform correct key comparison for server side joins
            static Expression CreateAnonymousObject(List<Expression> expressions)
                => Expression.New(
#pragma warning disable EF1001 // Internal EF Core API usage.
                    // #20565
                    AnonymousObject.AnonymousObjectCtor,
#pragma warning restore EF1001 // Internal EF Core API usage.
                    Expression.NewArrayInit(
                        typeof(object),
                        expressions.Select(e => Expression.Convert(e, typeof(object)))));
        }

        private static bool ProcessJoinCondition(
            Expression joinCondition,
            List<Expression> leftExpressions,
            List<Expression> rightExpressions)
        {
            if (joinCondition is BinaryExpression binaryExpression)
            {
                if (binaryExpression.NodeType == ExpressionType.Equal)
                {
                    leftExpressions.Add(binaryExpression.Left);
                    rightExpressions.Add(binaryExpression.Right);

                    return true;
                }

                if (binaryExpression.NodeType == ExpressionType.AndAlso)
                {
                    return ProcessJoinCondition(binaryExpression.Left, leftExpressions, rightExpressions)
                        && ProcessJoinCondition(binaryExpression.Right, leftExpressions, rightExpressions);
                }
            }

            return false;
        }

        private static (LambdaExpression OuterKeySelector, LambdaExpression InnerKeySelector)
            AlignKeySelectorTypes(LambdaExpression outerKeySelector, LambdaExpression innerKeySelector)
        {
            if (outerKeySelector.Body.Type != innerKeySelector.Body.Type)
            {
                if (IsConvertedToNullable(outerKeySelector.Body, innerKeySelector.Body))
                {
                    innerKeySelector = Expression.Lambda(
                        Expression.Convert(innerKeySelector.Body, outerKeySelector.Body.Type), innerKeySelector.Parameters);
                }
                else if (IsConvertedToNullable(innerKeySelector.Body, outerKeySelector.Body))
                {
                    outerKeySelector = Expression.Lambda(
                        Expression.Convert(outerKeySelector.Body, innerKeySelector.Body.Type), outerKeySelector.Parameters);
                }
            }

            return (outerKeySelector, innerKeySelector);

            static bool IsConvertedToNullable(Expression outer, Expression inner)
                => outer.Type.IsNullableType()
                    && !inner.Type.IsNullableType()
                    && outer.Type.UnwrapNullableType() == inner.Type;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateLastOrDefault(
            ShapedQueryExpression source,
            LambdaExpression predicate,
            Type returnType,
            bool returnDefault)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(returnType, nameof(returnType));

            return TranslateSingleResultOperator(
                source,
                predicate,
                returnType,
                returnDefault
                    ? EnumerableMethods.LastOrDefaultWithoutPredicate
                    : EnumerableMethods.LastWithoutPredicate);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateLeftJoin(
            ShapedQueryExpression outer,
            ShapedQueryExpression inner,
            LambdaExpression outerKeySelector,
            LambdaExpression innerKeySelector,
            LambdaExpression resultSelector)
        {
            Check.NotNull(outer, nameof(outer));
            Check.NotNull(inner, nameof(inner));
            Check.NotNull(resultSelector, nameof(resultSelector));

            (outerKeySelector, innerKeySelector) = ProcessJoinKeySelector(outer, inner, outerKeySelector, innerKeySelector);

            if (outerKeySelector == null
                || innerKeySelector == null)
            {
                return null;
            }

            var transparentIdentifierType = TransparentIdentifierFactory.Create(
                resultSelector.Parameters[0].Type,
                resultSelector.Parameters[1].Type);

            ((UbigiaQueryExpression)outer.QueryExpression).AddLeftJoin(
                (UbigiaQueryExpression)inner.QueryExpression,
                outerKeySelector,
                innerKeySelector,
                transparentIdentifierType);

#pragma warning disable CS0618 // Type or member is obsolete See issue#21200
            return TranslateResultSelectorForJoin(
                outer,
                resultSelector,
                MarkShaperNullable(inner.ShaperExpression),
                transparentIdentifierType);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateLongCount(ShapedQueryExpression source, LambdaExpression predicate)
        {
            Check.NotNull(source, nameof(source));

            if (predicate != null)
            {
                source = TranslateWhere(source, predicate);
                if (source == null)
                {
                    return null;
                }
            }

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            if (source.ShaperExpression is GroupByShaperExpression)
            {
                ubigiaQueryExpression.ReplaceProjectionMapping(new Dictionary<ProjectionMember, Expression>());
                ubigiaQueryExpression.PushdownIntoSubquery();
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.LongCountWithoutPredicate.MakeGenericMethod(
                        ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression));

            return source.UpdateShaperExpression(Expression.Convert(ubigiaQueryExpression.GetSingleScalarProjection(), typeof(long)));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateMax(
            ShapedQueryExpression source,
            LambdaExpression selector,
            Type resultType)
        {
            Check.NotNull(source, nameof(source));

            return TranslateScalarAggregate(source, selector, nameof(Enumerable.Max), resultType);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateMin(ShapedQueryExpression source, LambdaExpression selector, Type resultType)
        {
            Check.NotNull(source, nameof(source));

            return TranslateScalarAggregate(source, selector, nameof(Enumerable.Min), resultType);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateOfType(ShapedQueryExpression source, Type resultType)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(resultType, nameof(resultType));

            if (source.ShaperExpression is EntityShaperExpression entityShaperExpression)
            {
                var entityType = entityShaperExpression.EntityType;
                if (entityType.ClrType == resultType)
                {
                    return source;
                }

                var parameterExpression = Expression.Parameter(entityShaperExpression.Type);
                var predicate = Expression.Lambda(Expression.TypeIs(parameterExpression, resultType), parameterExpression);
                source = TranslateWhere(source, predicate);
                if (source == null)
                {
                    // EntityType is not part of hierarchy
                    return null;
                }

                var baseType = entityType.GetAllBaseTypes().SingleOrDefault(et => et.ClrType == resultType);
                if (baseType != null)
                {
                    return source.UpdateShaperExpression(entityShaperExpression.WithEntityType(baseType));
                }

                var derivedType = entityType.GetDerivedTypes().Single(et => et.ClrType == resultType);
                var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

                var projectionBindingExpression = (ProjectionBindingExpression)entityShaperExpression.ValueBufferExpression;
                var projectionMember = projectionBindingExpression.ProjectionMember;
                Check.DebugAssert(new ProjectionMember().Equals(projectionMember), "Invalid ProjectionMember when processing OfType");

                var entityProjectionExpression = (EntityProjectionExpression)ubigiaQueryExpression.GetMappedProjection(projectionMember);
                ubigiaQueryExpression.ReplaceProjectionMapping(
                    new Dictionary<ProjectionMember, Expression>
                    {
                        { projectionMember, entityProjectionExpression.UpdateEntityType(derivedType) }
                    });

                return source.UpdateShaperExpression(entityShaperExpression.WithEntityType(derivedType));
            }

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateOrderBy(
            ShapedQueryExpression source,
            LambdaExpression keySelector,
            bool ascending)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            keySelector = TranslateLambdaExpression(source, keySelector);
            if (keySelector == null)
            {
                return null;
            }

            var orderBy = ascending ? EnumerableMethods.OrderBy : EnumerableMethods.OrderByDescending;
            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    orderBy.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type, keySelector.ReturnType),
                    ubigiaQueryExpression.ServerQueryExpression,
                    keySelector));

            return source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateReverse(ShapedQueryExpression source)
        {
            Check.NotNull(source, nameof(source));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.Reverse.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression));

            return source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSelect(ShapedQueryExpression source, LambdaExpression selector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(selector, nameof(selector));

            if (selector.Body == selector.Parameters[0])
            {
                return source;
            }

            var newSelectorBody = ReplacingExpressionVisitor.Replace(
                selector.Parameters.Single(), source.ShaperExpression, selector.Body);

            var groupByQuery = source.ShaperExpression is GroupByShaperExpression;
            var queryExpression = (UbigiaQueryExpression)source.QueryExpression;

            var newShaper = _projectionBindingExpressionVisitor.Translate(queryExpression, newSelectorBody);
            if (groupByQuery)
            {
                queryExpression.PushdownIntoSubquery();
            }

            return source.UpdateShaperExpression(newShaper);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSelectMany(
            ShapedQueryExpression source,
            LambdaExpression collectionSelector,
            LambdaExpression resultSelector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(collectionSelector, nameof(collectionSelector));
            Check.NotNull(resultSelector, nameof(resultSelector));

            var defaultIfEmpty = new DefaultIfEmptyFindingExpressionVisitor().IsOptional(collectionSelector);
            var collectionSelectorBody = RemapLambdaBody(source, collectionSelector);

            if (Visit(collectionSelectorBody) is ShapedQueryExpression inner)
            {
                var transparentIdentifierType = TransparentIdentifierFactory.Create(
                    resultSelector.Parameters[0].Type,
                    resultSelector.Parameters[1].Type);

                var innerShaperExpression = defaultIfEmpty
                    ? MarkShaperNullable(inner.ShaperExpression)
                    : inner.ShaperExpression;

                ((UbigiaQueryExpression)source.QueryExpression).AddSelectMany(
                    (UbigiaQueryExpression)inner.QueryExpression, transparentIdentifierType, defaultIfEmpty);

#pragma warning disable CS0618 // Type or member is obsolete See issue#21200
                return TranslateResultSelectorForJoin(
                    source,
                    resultSelector,
                    innerShaperExpression,
                    transparentIdentifierType);
#pragma warning restore CS0618 // Type or member is obsolete
            }

            return null;
        }

        private sealed class DefaultIfEmptyFindingExpressionVisitor : ExpressionVisitor
        {
            private bool _defaultIfEmpty;

            public bool IsOptional(LambdaExpression lambdaExpression)
            {
                _defaultIfEmpty = false;

                Visit(lambdaExpression.Body);

                return _defaultIfEmpty;
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                Check.NotNull(node, nameof(node));

                if (node.Method.IsGenericMethod
                    && node.Method.GetGenericMethodDefinition() == QueryableMethods.DefaultIfEmptyWithoutArgument)
                {
                    _defaultIfEmpty = true;
                }

                return base.VisitMethodCall(node);
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSelectMany(ShapedQueryExpression source, LambdaExpression selector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(selector, nameof(selector));

            var innerParameter = Expression.Parameter(selector.ReturnType.TryGetSequenceType(), "i");
            var resultSelector = Expression.Lambda(
                innerParameter, Expression.Parameter(source.Type.TryGetSequenceType()), innerParameter);

            return TranslateSelectMany(source, selector, resultSelector);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSingleOrDefault(
            ShapedQueryExpression source,
            LambdaExpression predicate,
            Type returnType,
            bool returnDefault)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(returnType, nameof(returnType));

            return TranslateSingleResultOperator(
                source,
                predicate,
                returnType,
                returnDefault
                    ? EnumerableMethods.SingleOrDefaultWithoutPredicate
                    : EnumerableMethods.SingleWithoutPredicate);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSkip(ShapedQueryExpression source, Expression count)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(count, nameof(count));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;
            count = TranslateExpression(count);
            if (count == null)
            {
                return null;
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.Skip.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression,
                    count));

            return source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSkipWhile(ShapedQueryExpression source, LambdaExpression predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateSum(ShapedQueryExpression source, LambdaExpression selector, Type resultType)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(resultType, nameof(resultType));

            return TranslateScalarAggregate(source, selector, nameof(Enumerable.Sum), resultType);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateTake(ShapedQueryExpression source, Expression count)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(count, nameof(count));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;
            count = TranslateExpression(count);
            if (count == null)
            {
                return null;
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.Take.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression,
                    count));

            return source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateTakeWhile(ShapedQueryExpression source, LambdaExpression predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            return null;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateThenBy(ShapedQueryExpression source, LambdaExpression keySelector, bool ascending)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;
            keySelector = TranslateLambdaExpression(source, keySelector);
            if (keySelector == null)
            {
                return null;
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    (ascending ? EnumerableMethods.ThenBy : EnumerableMethods.ThenByDescending)
                    .MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type, keySelector.ReturnType),
                    ubigiaQueryExpression.ServerQueryExpression,
                    keySelector));

            return source;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateUnion(ShapedQueryExpression source1, ShapedQueryExpression source2)
        {
            Check.NotNull(source1, nameof(source1));
            Check.NotNull(source2, nameof(source2));

            return TranslateSetOperation(EnumerableMethods.Union, source1, source2);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override ShapedQueryExpression TranslateWhere(ShapedQueryExpression source, LambdaExpression predicate)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(predicate, nameof(predicate));

            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;
            predicate = TranslateLambdaExpression(source, predicate, preserveType: true);
            if (predicate == null)
            {
                return null;
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    EnumerableMethods.Where.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression,
                    predicate));

            return source;
        }

        private Expression TranslateExpression(Expression expression, bool preserveType = false)
        {
            var translation = _expressionTranslator.Translate(expression);
            if (translation == null && _expressionTranslator.TranslationErrorDetails != null)
            {
                AddTranslationErrorDetails(_expressionTranslator.TranslationErrorDetails);
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (expression != null
                && translation != null
                && preserveType
                && expression.Type != translation.Type)
            {
                translation = expression.Type == typeof(bool)
                    ? Expression.Equal(translation, Expression.Constant(true, translation.Type))
                    : (Expression)Expression.Convert(translation, expression.Type);
            }

            return translation;
        }

        private LambdaExpression TranslateLambdaExpression(
            ShapedQueryExpression shapedQueryExpression,
            LambdaExpression lambdaExpression,
            bool preserveType = false)
        {
            var lambdaBody = TranslateExpression(RemapLambdaBody(shapedQueryExpression, lambdaExpression), preserveType);

            return lambdaBody != null
                ? Expression.Lambda(
                    lambdaBody,
                    ((UbigiaQueryExpression)shapedQueryExpression.QueryExpression).CurrentParameter)
                : null;
        }

        private Expression RemapLambdaBody(ShapedQueryExpression shapedQueryExpression, LambdaExpression lambdaExpression)
        {
            var lambdaBody = ReplacingExpressionVisitor.Replace(
                lambdaExpression.Parameters.Single(), shapedQueryExpression.ShaperExpression, lambdaExpression.Body);

            return ExpandWeakEntities((UbigiaQueryExpression)shapedQueryExpression.QueryExpression, lambdaBody);
        }

        internal Expression ExpandWeakEntities(UbigiaQueryExpression queryExpression, Expression lambdaBody)
            => _weakEntityExpandingExpressionVisitor.Expand(queryExpression, lambdaBody);

        private sealed class WeakEntityExpandingExpressionVisitor : ExpressionVisitor
        {
            private UbigiaQueryExpression _queryExpression;
            private readonly UbigiaExpressionTranslatingExpressionVisitor _expressionTranslator;

            public WeakEntityExpandingExpressionVisitor(UbigiaExpressionTranslatingExpressionVisitor expressionTranslator)
            {
                _expressionTranslator = expressionTranslator;
            }

            // ReSharper disable once UnusedMember.Local
            public string TranslationErrorDetails => _expressionTranslator.TranslationErrorDetails;

            public Expression Expand(UbigiaQueryExpression queryExpression, Expression lambdaBody)
            {
                _queryExpression = queryExpression;

                return Visit(lambdaBody);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                Check.NotNull(node, nameof(node));

                var innerExpression = Visit(node.Expression);

                return TryExpand(innerExpression, MemberIdentity.Create(node.Member))
                    ?? node.Update(innerExpression!);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                Check.NotNull(node, nameof(node));

                if (node.TryGetEFPropertyArguments(out var source, out var navigationName))
                {
                    source = Visit(source);

                    return TryExpand(source, MemberIdentity.Create(navigationName))
                        ?? node.Update(null, new[] { source, node.Arguments[1] });
                }

                if (node.TryGetEFPropertyArguments(out source, out navigationName))
                {
                    source = Visit(source);

                    return TryExpand(source, MemberIdentity.Create(navigationName))
                        ?? node.Update(source, new[] { node.Arguments[0] });
                }

                return base.VisitMethodCall(node);
            }

            protected override Expression VisitExtension(Expression node)
            {
                Check.NotNull(node, nameof(node));

                return node is EntityShaperExpression
                    || node is ShapedQueryExpression
                    ? node
                    : base.VisitExtension(node);
            }

            private Expression TryExpand(Expression source, MemberIdentity member)
            {
                source = source.UnwrapTypeConversion(out var convertedType);
                if (!(source is EntityShaperExpression entityShaperExpression))
                {
                    return null;
                }

                var entityType = entityShaperExpression.EntityType;
                if (convertedType != null)
                {
                    entityType = entityType.GetRootType().GetDerivedTypesInclusive()
                        .FirstOrDefault(et => et.ClrType == convertedType);

                    if (entityType == null)
                    {
                        return null;
                    }
                }

                var navigation = member.MemberInfo != null
                    ? entityType.FindNavigation(member.MemberInfo)
                    : entityType.FindNavigation(member.Name);

                if (navigation == null)
                {
                    return null;
                }

                var targetEntityType = navigation.TargetEntityType;
                if (targetEntityType == null
                    || (!targetEntityType.HasDefiningNavigation()
                        && !targetEntityType.IsOwned()))
                {
                    return null;
                }

                var foreignKey = navigation.ForeignKey;
                if (navigation.IsCollection)
                {
                    var innerShapedQuery = CreateShapedQueryExpressionStatic(targetEntityType);
                    var innerQueryExpression = (UbigiaQueryExpression)innerShapedQuery.QueryExpression;

                    var makeNullable = foreignKey.PrincipalKey.Properties
                        .Concat(foreignKey.Properties)
                        .Select(p => p.ClrType)
                        .Any(t => t.IsNullableType());

                    var outerKey = entityShaperExpression.CreateKeyValuesExpression(
                        navigation.IsOnDependent
                            ? foreignKey.Properties
                            : foreignKey.PrincipalKey.Properties,
                        makeNullable);
                    var innerKey = innerShapedQuery.ShaperExpression.CreateKeyValuesExpression(
                        navigation.IsOnDependent
                            ? foreignKey.PrincipalKey.Properties
                            : foreignKey.Properties,
                        makeNullable);

                    var outerKeyFirstProperty = outerKey is NewExpression newExpression
                        ? ((UnaryExpression)((NewArrayExpression)newExpression.Arguments[0]).Expressions[0]).Operand
                        : outerKey;

                    var predicate = outerKeyFirstProperty.Type.IsNullableType()
                        ? Expression.AndAlso(
                            Expression.NotEqual(outerKeyFirstProperty, Expression.Constant(null, outerKeyFirstProperty.Type)),
                            Expression.Equal(outerKey, innerKey))
                        : Expression.Equal(outerKey, innerKey);

                    var correlationPredicate = _expressionTranslator.Translate(predicate);
                    innerQueryExpression.UpdateServerQueryExpression(
                        Expression.Call(
                            EnumerableMethods.Where.MakeGenericMethod(innerQueryExpression.CurrentParameter.Type),
                            innerQueryExpression.ServerQueryExpression,
                            Expression.Lambda(correlationPredicate, innerQueryExpression.CurrentParameter)));

                    return innerShapedQuery;
                }

                var entityProjectionExpression
                    = (EntityProjectionExpression)(entityShaperExpression.ValueBufferExpression is
                        ProjectionBindingExpression projectionBindingExpression
                        ? _queryExpression.GetMappedProjection(projectionBindingExpression.ProjectionMember)
                        : entityShaperExpression.ValueBufferExpression);

                var innerShaper = entityProjectionExpression.BindNavigation(navigation);
                if (innerShaper == null)
                {
                    var innerShapedQuery = CreateShapedQueryExpressionStatic(targetEntityType);
                    var innerQueryExpression = (UbigiaQueryExpression)innerShapedQuery.QueryExpression;

                    var makeNullable = foreignKey.PrincipalKey.Properties
                        .Concat(foreignKey.Properties)
                        .Select(p => p.ClrType)
                        .Any(t => t.IsNullableType());

                    var outerKey = entityShaperExpression.CreateKeyValuesExpression(
                        navigation.IsOnDependent
                            ? foreignKey.Properties
                            : foreignKey.PrincipalKey.Properties,
                        makeNullable);
                    var innerKey = innerShapedQuery.ShaperExpression.CreateKeyValuesExpression(
                        navigation.IsOnDependent
                            ? foreignKey.PrincipalKey.Properties
                            : foreignKey.Properties,
                        makeNullable);

                    var outerKeySelector = Expression.Lambda(_expressionTranslator.Translate(outerKey), _queryExpression.CurrentParameter);
                    var innerKeySelector = Expression.Lambda(
                        _expressionTranslator.Translate(innerKey), innerQueryExpression.CurrentParameter);
                    (outerKeySelector, innerKeySelector) = AlignKeySelectorTypes(outerKeySelector, innerKeySelector);
                    innerShaper = _queryExpression.AddNavigationToWeakEntityType(
                        entityProjectionExpression, navigation, innerQueryExpression, outerKeySelector, innerKeySelector);
                }

                return innerShaper;
            }
        }

        private ShapedQueryExpression TranslateScalarAggregate(
            ShapedQueryExpression source,
            LambdaExpression selector,
            string methodName,
            Type returnType)
        {
            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            selector = selector == null
                || selector.Body == selector.Parameters[0]
                    ? Expression.Lambda(
                        ubigiaQueryExpression.GetMappedProjection(new ProjectionMember()),
                        ubigiaQueryExpression.CurrentParameter)
                    : TranslateLambdaExpression(source, selector, preserveType: true);

            if (selector == null)
            {
                return null;
            }

            var method = GetMethod();
            method = method.GetGenericArguments().Length == 2
                ? method.MakeGenericMethod(typeof(ValueBuffer), selector.ReturnType)
                : method.MakeGenericMethod(typeof(ValueBuffer));

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(method, ubigiaQueryExpression.ServerQueryExpression, selector));

            return source.UpdateShaperExpression(Expression.Convert(ubigiaQueryExpression.GetSingleScalarProjection(), returnType));

            MethodInfo GetMethod()
                => methodName switch
                {
                    nameof(Enumerable.Average) => EnumerableMethods.GetAverageWithSelector(selector.ReturnType),
                    nameof(Enumerable.Max) => EnumerableMethods.GetMaxWithSelector(selector.ReturnType),
                    nameof(Enumerable.Min) => EnumerableMethods.GetMinWithSelector(selector.ReturnType),
                    nameof(Enumerable.Sum) => EnumerableMethods.GetSumWithSelector(selector.ReturnType),
                    _ => throw new InvalidOperationException(CoreStrings.UnknownEntity("Aggregate Operator")),
                };
        }

        private ShapedQueryExpression TranslateSingleResultOperator(
            ShapedQueryExpression source,
            LambdaExpression predicate,
            Type returnType,
            MethodInfo method)
        {
            var ubigiaQueryExpression = (UbigiaQueryExpression)source.QueryExpression;

            if (predicate != null)
            {
                source = TranslateWhere(source, predicate);
                if (source == null)
                {
                    return null;
                }
            }

            ubigiaQueryExpression.UpdateServerQueryExpression(
                Expression.Call(
                    method.MakeGenericMethod(ubigiaQueryExpression.CurrentParameter.Type),
                    ubigiaQueryExpression.ServerQueryExpression));

            ubigiaQueryExpression.ConvertToEnumerable();

            return source.ShaperExpression.Type != returnType
                ? source.UpdateShaperExpression(Expression.Convert(source.ShaperExpression, returnType))
                : source;
        }

        private ShapedQueryExpression TranslateSetOperation(
            MethodInfo setOperationMethodInfo,
            ShapedQueryExpression source1,
            ShapedQueryExpression source2)
        {
            var ubigiaQueryExpression1 = (UbigiaQueryExpression)source1.QueryExpression;
            var ubigiaQueryExpression2 = (UbigiaQueryExpression)source2.QueryExpression;

            // Apply any pending selectors, ensuring that the shape of both expressions is identical
            // prior to applying the set operation.
            ubigiaQueryExpression1.PushdownIntoSubquery();
            ubigiaQueryExpression2.PushdownIntoSubquery();

            ubigiaQueryExpression1.UpdateServerQueryExpression(
                Expression.Call(
                    setOperationMethodInfo.MakeGenericMethod(typeof(ValueBuffer)),
                    ubigiaQueryExpression1.ServerQueryExpression,
                    ubigiaQueryExpression2.ServerQueryExpression));

            return source1;
        }
    }
}
