// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// This code will change. Remove these pragma afterwards.
#pragma warning disable S3358
#pragma warning disable S1066 // Collapsible "if" statements should be merged.
#pragma warning disable S1168 // Empty arrays and collections should be returned instead of null.
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high.
#pragma warning disable S1168 // Empty arrays and collections should be returned instead of null.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Query.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Utilities;
    using ExpressionExtensions = Microsoft.EntityFrameworkCore.Infrastructure.ExpressionExtensions;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaExpressionTranslatingExpressionVisitor : ExpressionVisitor
    {
        private const string _runtimeParameterPrefix = QueryCompilationContext.QueryParameterPrefix + "entity_equality_";

        private static readonly MemberInfo _valueBufferIsEmpty = typeof(ValueBuffer).GetMember(nameof(ValueBuffer.IsEmpty))[0];

        private static readonly MethodInfo _parameterValueExtractorMethodInfo = typeof(UbigiaExpressionTranslatingExpressionVisitor).GetTypeInfo().GetDeclaredMethod(nameof(ParameterValueExtractor));

        private static readonly MethodInfo _parameterListValueExtractorMethodInfo = typeof(UbigiaExpressionTranslatingExpressionVisitor).GetTypeInfo().GetDeclaredMethod(nameof(ParameterListValueExtractor));

        private static readonly MethodInfo _getParameterValueMethodInfo = typeof(UbigiaExpressionTranslatingExpressionVisitor).GetTypeInfo().GetDeclaredMethod(nameof(GetParameterValue));

        private static readonly MethodInfo _likeMethodInfo = typeof(DbFunctionsExtensions).GetRuntimeMethod(
            nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) });

        private static readonly MethodInfo _likeMethodInfoWithEscape = typeof(DbFunctionsExtensions).GetRuntimeMethod(
            nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string), typeof(string) });

        private static readonly MethodInfo _ubigiaLikeMethodInfo = typeof(UbigiaExpressionTranslatingExpressionVisitor).GetTypeInfo().GetDeclaredMethod(nameof(UbigiaLike));

        // Regex special chars defined here:
        // https://msdn.microsoft.com/en-us/library/4edbef7e(v=vs.110).aspx
        private static readonly char[] _regexSpecialChars = { '.', '$', '^', '{', '[', '(', '|', ')', '*', '+', '?', '\\' };

        private static readonly string _defaultEscapeRegexCharsPattern = BuildEscapeRegexCharsPattern(_regexSpecialChars);

        private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(value: 1000.0);

        private static string BuildEscapeRegexCharsPattern(IEnumerable<char> regexSpecialChars) => string.Join("|", regexSpecialChars.Select(c => @"\" + c));

        private readonly QueryCompilationContext _queryCompilationContext;
        private readonly QueryableMethodTranslatingExpressionVisitor _queryableMethodTranslatingExpressionVisitor;
        private readonly EntityReferenceFindingExpressionVisitor _entityReferenceFindingExpressionVisitor;
        private readonly IModel _model;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaExpressionTranslatingExpressionVisitor(
            [NotNull] QueryCompilationContext queryCompilationContext,
            [NotNull] QueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor)
        {
            _queryCompilationContext = queryCompilationContext;
            _queryableMethodTranslatingExpressionVisitor = queryableMethodTranslatingExpressionVisitor;
            _entityReferenceFindingExpressionVisitor = new EntityReferenceFindingExpressionVisitor();
            _model = queryCompilationContext.Model;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual string TranslationErrorDetails { get; private set; }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected virtual void AddTranslationErrorDetails([NotNull] string details)
        {
            Check.NotNull(details, nameof(details));

            if (TranslationErrorDetails == null)
            {
                TranslationErrorDetails = details;
            }
            else
            {
                TranslationErrorDetails += Environment.NewLine + details;
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual Expression Translate([NotNull] Expression expression)
        {
            Check.NotNull(expression, nameof(expression));

            TranslationErrorDetails = null;

            return TranslateInternal(expression);
        }

        private Expression TranslateInternal(Expression expression)
        {
            var result = Visit(expression);

            return _entityReferenceFindingExpressionVisitor.Find(result)
                ? null
                : result;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Check.NotNull(node, nameof(node));

            if (node.Left.Type == typeof(object[])
                && node.Left is NewArrayExpression
                && node.NodeType == ExpressionType.Equal)
            {
                return Visit(ConvertObjectArrayEqualityComparison(node.Left, node.Right))!;
            }

            var newLeft = Visit(node.Left);
            var newRight = Visit(node.Right);

            if (newLeft == null
                || newRight == null)
            {
                return null!;
            }

            if ((node.NodeType == ExpressionType.Equal
                    || node.NodeType == ExpressionType.NotEqual)
                // Visited expression could be null, We need to pass MemberInitExpression
                && TryRewriteEntityEquality(
                    node.NodeType,
                    // ReSharper disable ConstantNullCoalescingCondition
                    newLeft ?? node.Left,
                    newRight ?? node.Right,
                    // ReSharper restore ConstantNullCoalescingCondition
                    equalsMethod: false,
                    out var result))
            {
                return result;
            }

            if (IsConvertedToNullable(newLeft, node.Left)
                || IsConvertedToNullable(newRight, node.Right))
            {
                newLeft = ConvertToNullable(newLeft);
                newRight = ConvertToNullable(newRight);
            }

            if (node.NodeType == ExpressionType.Equal
                || node.NodeType == ExpressionType.NotEqual)
            {
                var property = FindProperty(newLeft) ?? FindProperty(newRight);
                if (property != null)
                {
                    var comparer = property.GetValueComparer();

                    if (comparer != null
                        && comparer.Type.IsAssignableFrom(newLeft.Type)
                        && comparer.Type.IsAssignableFrom(newRight.Type))
                    {
                        if (node.NodeType == ExpressionType.Equal)
                        {
                            return comparer.ExtractEqualsBody(newLeft, newRight);
                        }

                        if (node.NodeType == ExpressionType.NotEqual)
                        {
                            return Expression.IsFalse(comparer.ExtractEqualsBody(newLeft, newRight));
                        }
                    }
                }
            }

            return Expression.MakeBinary(
                node.NodeType,
                newLeft,
                newRight,
                node.IsLiftedToNull,
                node.Method,
                node.Conversion);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Check.NotNull(node, nameof(node));

            var test = Visit(node.Test);
            var ifTrue = Visit(node.IfTrue);
            var ifFalse = Visit(node.IfFalse);

            if (test == null
                || ifTrue == null
                || ifFalse == null)
            {
                return null!;
            }

            if (test.Type == typeof(bool?))
            {
                test = Expression.Equal(test, Expression.Constant(true, typeof(bool?)));
            }

            if (IsConvertedToNullable(ifTrue, node.IfTrue)
                || IsConvertedToNullable(ifFalse, node.IfFalse))
            {
                ifTrue = ConvertToNullable(ifTrue);
                ifFalse = ConvertToNullable(ifFalse);
            }

            return Expression.Condition(test, ifTrue, ifFalse);
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

            switch (node)
            {
                case EntityProjectionExpression _:
                case EntityReferenceExpression _:
                    return node;

                case EntityShaperExpression entityShaperExpression:
                    return new EntityReferenceExpression(entityShaperExpression);

                case ProjectionBindingExpression projectionBindingExpression:
                    return projectionBindingExpression.ProjectionMember != null
                        ? ((UbigiaQueryExpression)projectionBindingExpression.QueryExpression)
                        .GetMappedProjection(projectionBindingExpression.ProjectionMember)
                        : null;

                case UbigiaGroupByShaperExpression ubigiaGroupByShaperExpression:
                    return new GroupingElementExpression(
                        ubigiaGroupByShaperExpression.GroupingParameter,
                        ubigiaGroupByShaperExpression.ElementSelector,
                        ubigiaGroupByShaperExpression.ValueBufferParameter);

                default:
                    return null;
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitInvocation(InvocationExpression node) => null!;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitLambda<T>(Expression<T> node) => null!;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitListInit(ListInitExpression node) => null!;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitMember(MemberExpression node)
        {
            Check.NotNull(node, nameof(node));

            var innerExpression = Visit(node.Expression);
            if (node.Expression != null
                && innerExpression == null)
            {
                return null!;
            }

            var result = TryBindMember(innerExpression, MemberIdentity.Create(node.Member), node.Type);
            if (result != null)
            {
                return result;
            }

            var updatedMemberExpression = (Expression)node.Update(innerExpression!);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (innerExpression != null
                && innerExpression.Type.IsNullableType()
                && ShouldApplyNullProtectionForMemberAccess(innerExpression.Type, node.Member.Name))
            {
                updatedMemberExpression = ConvertToNullable(updatedMemberExpression);

                return Expression.Condition(
                    Expression.Equal(innerExpression, Expression.Default(innerExpression.Type)),
                    Expression.Default(updatedMemberExpression.Type),
                    updatedMemberExpression);
            }

            return updatedMemberExpression;

            static bool ShouldApplyNullProtectionForMemberAccess(Type callerType, string memberName)
                => !(callerType.IsGenericType
                    && callerType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && (memberName == nameof(Nullable<int>.Value) || memberName == nameof(Nullable<int>.HasValue)));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            var expression = Visit(node.Expression);
            if (expression == null)
            {
                return null!;
            }

            if (IsConvertedToNullable(expression, node.Expression))
            {
                expression = ConvertToNonNullable(expression);
            }

            return node.Update(expression);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Check.NotNull(node, nameof(node));

            if (node.Method.IsGenericMethod
                && node.Method.GetGenericMethodDefinition() == ExpressionExtensions.ValueBufferTryReadValueMethod)
            {
                return node;
            }

            // EF.Property case
            if (node.TryGetEFPropertyArguments(out var source, out var propertyName))
            {
                return TryBindMember(Visit(source), MemberIdentity.Create(propertyName), node.Type)
                    ?? throw new InvalidOperationException(CoreStrings.QueryUnableToTranslateEFProperty(node.Print()));
            }

            // EF Indexer property
            if (node.TryGetIndexerArguments(_model, out source, out propertyName))
            {
                return TryBindMember(Visit(source), MemberIdentity.Create(propertyName), node.Type);
            }

            // GroupBy Aggregate case
            if (node.Object == null
                && node.Method.DeclaringType == typeof(Enumerable)
                && node.Arguments.Count > 0)
            {
                if (node.Arguments[0].Type.TryGetElementType(typeof(IQueryable<>)) == null
                    && Visit(node.Arguments[0]) is GroupingElementExpression groupingElementExpression)
                {
                    Expression result = null;
                    switch (node.Method.Name)
                    {
                        case nameof(Enumerable.Average):
                        {
                            if (node.Arguments.Count == 2)
                            {
                                groupingElementExpression = ApplySelector(
                                    groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());
                            }

                            var expression = ApplySelect(groupingElementExpression);

                            result = expression == null
                                ? null
                                : Expression.Call(
                                    EnumerableMethods.GetAverageWithoutSelector(expression.Type.TryGetSequenceType()), expression);
                            break;
                        }

                        case nameof(Enumerable.Count):
                        {
                            if (node.Arguments.Count == 2)
                            {
                                groupingElementExpression = ApplyPredicate(
                                    groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());

                                if (groupingElementExpression == null)
                                {
                                    result = null;
                                    break;
                                }
                            }

                            var expression = ApplySelect(groupingElementExpression);

                            result = expression == null
                                ? null
                                : Expression.Call(
                                    EnumerableMethods.CountWithoutPredicate.MakeGenericMethod(expression.Type.TryGetSequenceType()),
                                    expression);
                            break;
                        }

                        case nameof(Enumerable.Distinct):
                            result = groupingElementExpression.Selector is EntityShaperExpression
                                ? groupingElementExpression
                                : groupingElementExpression.IsDistinct
                                    ? null
                                    : groupingElementExpression.ApplyDistinct();
                            break;

                        case nameof(Enumerable.LongCount):
                        {
                            if (node.Arguments.Count == 2)
                            {
                                groupingElementExpression = ApplyPredicate(
                                    groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());

                                if (groupingElementExpression == null)
                                {
                                    result = null;
                                    break;
                                }
                            }

                            var expression = ApplySelect(groupingElementExpression);

                            result = expression == null
                                ? null
                                : Expression.Call(
                                    EnumerableMethods.LongCountWithoutPredicate.MakeGenericMethod(expression.Type.TryGetSequenceType()),
                                    expression);
                            break;
                        }

                        case nameof(Enumerable.Max):
                        {
                            if (node.Arguments.Count == 2)
                            {
                                groupingElementExpression = ApplySelector(
                                    groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());
                            }

                            var expression = ApplySelect(groupingElementExpression);
                            if (expression == null
                                || expression is ParameterExpression)
                            {
                                result = null;
                            }
                            else
                            {
                                var type = expression.Type.TryGetSequenceType();
                                var aggregateMethod = EnumerableMethods.GetMaxWithoutSelector(type);
                                if (aggregateMethod.IsGenericMethod)
                                {
                                    aggregateMethod = aggregateMethod.MakeGenericMethod(type);
                                }

                                result = Expression.Call(aggregateMethod, expression);
                            }

                            break;
                        }

                        case nameof(Enumerable.Min):
                        {
                            if (node.Arguments.Count == 2)
                            {
                                groupingElementExpression = ApplySelector(
                                    groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());
                            }

                            var expression = ApplySelect(groupingElementExpression);
                            if (expression == null
                                || expression is ParameterExpression)
                            {
                                result = null;
                            }
                            else
                            {
                                var type = expression.Type.TryGetSequenceType();
                                var aggregateMethod = EnumerableMethods.GetMinWithoutSelector(type);
                                if (aggregateMethod.IsGenericMethod)
                                {
                                    aggregateMethod = aggregateMethod.MakeGenericMethod(type);
                                }

                                result = Expression.Call(aggregateMethod, expression);
                            }

                            break;
                        }

                        case nameof(Enumerable.Select):
                            result = ApplySelector(groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());
                            break;

                        case nameof(Enumerable.Sum):
                        {
                            if (node.Arguments.Count == 2)
                            {
                                groupingElementExpression = ApplySelector(
                                    groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());
                            }

                            var expression = ApplySelect(groupingElementExpression);

                            result = expression == null
                                ? null
                                : Expression.Call(
                                    EnumerableMethods.GetSumWithoutSelector(expression.Type.TryGetSequenceType()), expression);
                            break;
                        }

                        case nameof(Enumerable.Where):
                            result = ApplyPredicate(groupingElementExpression, node.Arguments[1].UnwrapLambdaFromQuote());
                            break;

                        default:
                            result = null;
                            break;
                    }

                    return result ?? throw new InvalidOperationException(CoreStrings.TranslationFailed(node.Print()));

                    GroupingElementExpression ApplyPredicate(GroupingElementExpression groupingElement, LambdaExpression lambdaExpression)
                    {
                        var predicate = TranslateInternal(RemapLambda(groupingElement, lambdaExpression));

                        return predicate == null
                            ? null
                            : groupingElement.UpdateSource(
                                Expression.Call(
                                    EnumerableMethods.Where.MakeGenericMethod(typeof(ValueBuffer)),
                                    groupingElement.Source,
                                    Expression.Lambda(predicate, groupingElement.ValueBufferParameter)));
                    }

                    Expression ApplySelect(GroupingElementExpression groupingElement)
                    {
                        var selector = TranslateInternal(groupingElement.Selector);

                        if (selector == null)
                        {
                            return groupingElement.Selector is EntityShaperExpression
                                ? groupingElement.Source
                                : null;
                        }

                        var innerResult = Expression.Call(
                            EnumerableMethods.Select.MakeGenericMethod(typeof(ValueBuffer), selector.Type),
                            groupingElement.Source,
                            Expression.Lambda(selector, groupingElement.ValueBufferParameter));

                        if (groupingElement.IsDistinct)
                        {
                            innerResult = Expression.Call(
                                EnumerableMethods.Distinct.MakeGenericMethod(selector.Type),
                                innerResult);
                        }

                        return innerResult;
                    }

                    static GroupingElementExpression ApplySelector(
                        GroupingElementExpression groupingElement,
                        LambdaExpression lambdaExpression)
                    {
                        var selector = RemapLambda(groupingElement, lambdaExpression);

                        return groupingElement.ApplySelector(selector);
                    }

                    static Expression RemapLambda(GroupingElementExpression groupingElement, LambdaExpression lambdaExpression)
                        => ReplacingExpressionVisitor.Replace(
                            lambdaExpression.Parameters[0], groupingElement.Selector, lambdaExpression.Body);
                }
            }

            // Subquery case
            var subqueryTranslation = _queryableMethodTranslatingExpressionVisitor.TranslateSubquery(node);
            if (subqueryTranslation != null)
            {
                var subquery = (UbigiaQueryExpression)subqueryTranslation.QueryExpression;
                if (subqueryTranslation.ResultCardinality == ResultCardinality.Enumerable)
                {
                    return null!;
                }

                var shaperExpression = subqueryTranslation.ShaperExpression;
                var innerExpression = shaperExpression;
                Type convertedType = null;
                if (shaperExpression is UnaryExpression unaryExpression
                    && unaryExpression.NodeType == ExpressionType.Convert)
                {
                    convertedType = unaryExpression.Type;
                    innerExpression = unaryExpression.Operand;
                }

                if (innerExpression is EntityShaperExpression entityShaperExpression
                    && (convertedType == null
                        || convertedType.IsAssignableFrom(entityShaperExpression.Type)))
                {
                    return new EntityReferenceExpression(subqueryTranslation.UpdateShaperExpression(innerExpression));
                }

                if (!(innerExpression is ProjectionBindingExpression projectionBindingExpression
                    && (convertedType == null
                        || convertedType.MakeNullable() == innerExpression.Type)))
                {
                    return null!;
                }

                return ProcessSingleResultScalar(
                    subquery.ServerQueryExpression,
                    subquery.GetMappedProjection(projectionBindingExpression.ProjectionMember),
                    subquery.CurrentParameter,
                    node.Type);
            }

            if (node.Method == _likeMethodInfo
                || node.Method == _likeMethodInfoWithEscape)
            {
                // EF.Functions.Like
                var visitedArguments = new Expression[3];
                visitedArguments[2] = Expression.Constant(null, typeof(string));
                // Skip first DbFunctions argument
                for (var i = 1; i < node.Arguments.Count; i++)
                {
                    var argument = Visit(node.Arguments[i]);
                    if (TranslationFailed(node.Arguments[i], argument))
                    {
                        return null!;
                    }

                    visitedArguments[i - 1] = argument;
                }

                return Expression.Call(_ubigiaLikeMethodInfo, visitedArguments);
            }

            Expression @object = null;
            Expression[] arguments;
            var method = node.Method;

            if (method.Name == nameof(object.Equals)
                && node.Object != null
                && node.Arguments.Count == 1)
            {
                var left = Visit(node.Object);
                var right = Visit(node.Arguments[0]);

                if (TryRewriteEntityEquality(
                    ExpressionType.Equal,
                    left ?? node.Object,
                    right ?? node.Arguments[0],
                    equalsMethod: true,
                    out var result))
                {
                    return result;
                }

                if (TranslationFailed(left)
                    || TranslationFailed(right))
                {
                    return null!;
                }

                @object = left;
                arguments = new[] { right };
            }
            else if (method.Name == nameof(object.Equals)
                && node.Object == null
                && node.Arguments.Count == 2)
            {
                if (node.Arguments[0].Type == typeof(object[])
                    && node.Arguments[0] is NewArrayExpression)
                {
                    return Visit(
                        ConvertObjectArrayEqualityComparison(
                            node.Arguments[0], node.Arguments[1]))!;
                }

                var left = Visit(node.Arguments[0]);
                var right = Visit(node.Arguments[1]);

                if (TryRewriteEntityEquality(
                    ExpressionType.Equal,
                    left ?? node.Arguments[0],
                    right ?? node.Arguments[1],
                    equalsMethod: true,
                    out var result))
                {
                    return result;
                }

                if (TranslationFailed(left)
                    || TranslationFailed(right))
                {
                    return null!;
                }

                arguments = new[] { left, right };
            }
            else if (method.IsGenericMethod
                && method.GetGenericMethodDefinition().Equals(EnumerableMethods.Contains))
            {
                var enumerable = Visit(node.Arguments[0]);
                var item = Visit(node.Arguments[1]);

                if (TryRewriteContainsEntity(enumerable, item ?? node.Arguments[1], out var result))
                {
                    return result;
                }

                if (TranslationFailed(enumerable)
                    || TranslationFailed(item))
                {
                    return null!;
                }

                arguments = new[] { enumerable, item };
            }
            else if (node.Arguments.Count == 1
                && method.IsContainsMethod())
            {
                var enumerable = Visit(node.Object);
                var item = Visit(node.Arguments[0]);

                if (TryRewriteContainsEntity(enumerable, item ?? node.Arguments[0], out var result))
                {
                    return result;
                }

                if (TranslationFailed(enumerable)
                    || TranslationFailed(item))
                {
                    return null!;
                }

                @object = enumerable;
                arguments = new[] { item };
            }
            else
            {
                @object = Visit(node.Object);
                if (TranslationFailed(node.Object, @object))
                {
                    return null!;
                }

                arguments = new Expression[node.Arguments.Count];
                for (var i = 0; i < arguments.Length; i++)
                {
                    var argument = Visit(node.Arguments[i]);
                    if (TranslationFailed(node.Arguments[i], argument))
                    {
                        return null!;
                    }

                    arguments[i] = argument;
                }
            }

            // if the nullability of arguments change, we have no easy/reliable way to adjust the actual methodInfo to match the new type,
            // so we are forced to cast back to the original type
            var parameterTypes = node.Method.GetParameters().Select(p => p.ParameterType).ToArray();
            for (var i = 0; i < arguments.Length; i++)
            {
                var argument = arguments[i];
                if (IsConvertedToNullable(argument, node.Arguments[i])
                    && !parameterTypes[i].IsAssignableFrom(argument.Type))
                {
                    argument = ConvertToNonNullable(argument);
                }

                arguments[i] = argument;
            }

            // if object is nullable, add null safeguard before calling the function
            // we special-case Nullable<>.GetValueOrDefault, which doesn't need the safeguard
            if (node.Object != null
                && @object!.Type.IsNullableType()
                && node.Method.Name != nameof(Nullable<int>.GetValueOrDefault))
            {
                var result = (Expression)node.Update(
                    Expression.Convert(@object, node.Object.Type),
                    arguments);

                result = ConvertToNullable(result);
                result = Expression.Condition(
                    Expression.Equal(@object, Expression.Constant(null, @object.Type)),
                    Expression.Constant(null, result.Type),
                    result);

                return result;
            }

            return node.Update(@object, arguments);
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

            var newArguments = new List<Expression>();
            foreach (var argument in node.Arguments)
            {
                var newArgument = Visit(argument);
                if (newArgument == null)
                {
                    return null!;
                }

                if (IsConvertedToNullable(newArgument, argument))
                {
                    newArgument = ConvertToNonNullable(newArgument);
                }

                newArguments.Add(newArgument);
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
        {
            Check.NotNull(node, nameof(node));

            var newExpressions = new List<Expression>();
            foreach (var expression in node.Expressions)
            {
                var newExpression = Visit(expression);
                if (newExpression == null)
                {
                    return null!;
                }

                if (IsConvertedToNullable(newExpression, expression))
                {
                    newExpression = ConvertToNonNullable(newExpression);
                }

                newExpressions.Add(newExpression);
            }

            return node.Update(newExpressions);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            Check.NotNull(node, nameof(node));

            if (node.Name?.StartsWith(QueryCompilationContext.QueryParameterPrefix, StringComparison.Ordinal) == true)
            {
                return Expression.Call(
                    _getParameterValueMethodInfo.MakeGenericMethod(node.Type),
                    QueryCompilationContext.QueryContextParameter,
                    Expression.Constant(node.Name));
            }

            throw new InvalidOperationException(CoreStrings.TranslationFailed(node.Print()));
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            Check.NotNull(node, nameof(node));

            if (node.NodeType == ExpressionType.TypeIs
                && Visit(node.Expression) is EntityReferenceExpression entityReferenceExpression)
            {
                var entityType = entityReferenceExpression.EntityType;

                if (entityType.GetAllBaseTypesInclusive().Any(et => et.ClrType == node.TypeOperand))
                {
                    return Expression.Constant(true);
                }

                var derivedType = entityType.GetDerivedTypes().SingleOrDefault(et => et.ClrType == node.TypeOperand);
                if (derivedType != null)
                {
                    var discriminatorProperty = entityType.GetDiscriminatorProperty();
                    var boundProperty = BindProperty(entityReferenceExpression, discriminatorProperty, discriminatorProperty.ClrType);
                    var valueComparer = discriminatorProperty.GetKeyValueComparer();

                    var equals = valueComparer.ExtractEqualsBody(
                        boundProperty,
                        Expression.Constant(derivedType.GetDiscriminatorValue(), discriminatorProperty.ClrType));

                    foreach (var derivedDerivedType in derivedType.GetDerivedTypes())
                    {
                        equals = Expression.OrElse(
                            equals,
                            valueComparer.ExtractEqualsBody(
                                boundProperty,
                                Expression.Constant(derivedDerivedType.GetDiscriminatorValue(), discriminatorProperty.ClrType)));
                    }

                    return equals;
                }
            }

            return null!;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            Check.NotNull(node, nameof(node));

            var newOperand = Visit(node.Operand);
            if (newOperand == null)
            {
                return null!;
            }

            if (newOperand is EntityReferenceExpression entityReferenceExpression
                && (node.NodeType == ExpressionType.Convert
                    || node.NodeType == ExpressionType.ConvertChecked
                    || node.NodeType == ExpressionType.TypeAs))
            {
                return entityReferenceExpression.Convert(node.Type);
            }

            if (node.NodeType == ExpressionType.Convert
                && newOperand.Type == node.Type)
            {
                return newOperand;
            }

            if (node.NodeType == ExpressionType.Convert
                && IsConvertedToNullable(newOperand, node))
            {
                return newOperand;
            }

            var result = (Expression)Expression.MakeUnary(node.NodeType, newOperand, node.Type);
            if (result is UnaryExpression outerUnary
                && outerUnary.NodeType == ExpressionType.Convert
                && outerUnary.Operand is UnaryExpression innerUnary
                && innerUnary.NodeType == ExpressionType.Convert)
            {
                var innerMostType = innerUnary.Operand.Type;
                var intermediateType = innerUnary.Type;
                var outerMostType = outerUnary.Type;

                if (outerMostType == innerMostType
                    && intermediateType == innerMostType.UnwrapNullableType())
                {
                    result = innerUnary.Operand;
                }
                else if (outerMostType == typeof(object)
                    && intermediateType == innerMostType.UnwrapNullableType())
                {
                    result = Expression.Convert(innerUnary.Operand, typeof(object));
                }
            }

            return result;
        }

        private Expression TryBindMember(Expression source, MemberIdentity member, Type type)
        {
            if (!(source is EntityReferenceExpression entityReferenceExpression))
            {
                return null;
            }

            var entityType = entityReferenceExpression.EntityType;

            var property = member.MemberInfo != null
                ? entityType.FindProperty(member.MemberInfo)
                : entityType.FindProperty(member.Name);

            if (property != null)
            {
                return BindProperty(entityReferenceExpression, property, type);
            }

            AddTranslationErrorDetails(
                CoreStrings.QueryUnableToTranslateMember(
                    member.Name,
                    entityReferenceExpression.EntityType.DisplayName()));

            return null;
        }

        private Expression BindProperty(EntityReferenceExpression entityReferenceExpression, IProperty property, Type type)
        {
            if (entityReferenceExpression.ParameterEntity != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                var result = ((EntityProjectionExpression)Visit(entityReferenceExpression.ParameterEntity.ValueBufferExpression))
                    .BindProperty(property);

                // if the result type change was just nullability change e.g from int to int?
                // we want to preserve the new type for null propagation
                if (result.Type != type
                    && !(result.Type.IsNullableType()
                        && !type.IsNullableType()
                        && result.Type.UnwrapNullableType() == type))
                {
                    result = Expression.Convert(result, type);
                }

                return result;
            }

            if (entityReferenceExpression.SubqueryEntity != null)
            {
                var entityShaper = (EntityShaperExpression)entityReferenceExpression.SubqueryEntity.ShaperExpression;
                var ubigiaQueryExpression = (UbigiaQueryExpression)entityReferenceExpression.SubqueryEntity.QueryExpression;

                Expression readValueExpression;
                var projectionBindingExpression = (ProjectionBindingExpression)entityShaper.ValueBufferExpression;
                if (projectionBindingExpression.ProjectionMember != null)
                {
                    var entityProjectionExpression = (EntityProjectionExpression)ubigiaQueryExpression.GetMappedProjection(
                        projectionBindingExpression.ProjectionMember);
                    readValueExpression = entityProjectionExpression.BindProperty(property);
                }
                else
                {
                    // This has to be index map since entities cannot map to just integer index
                    var index = projectionBindingExpression.IndexMap[property];
                    readValueExpression = ubigiaQueryExpression.Projection[index];
                }

                return ProcessSingleResultScalar(
                    ubigiaQueryExpression.ServerQueryExpression,
                    readValueExpression,
                    ubigiaQueryExpression.CurrentParameter,
                    type);
            }

            return null;
        }

        private static Expression ProcessSingleResultScalar(
            Expression serverQuery,
            Expression readValueExpression,
            Expression valueBufferParameter,
            Type type)
        {
            var singleResult = ((LambdaExpression)((NewExpression)serverQuery).Arguments[0]).Body;
            if (readValueExpression is UnaryExpression unaryExpression
                && unaryExpression.NodeType == ExpressionType.Convert
                && unaryExpression.Type == typeof(object))
            {
                readValueExpression = unaryExpression.Operand;
            }

            var valueBufferVariable = Expression.Variable(typeof(ValueBuffer));
            var replacedReadExpression = ReplacingExpressionVisitor.Replace(
                valueBufferParameter,
                valueBufferVariable,
                readValueExpression);

            replacedReadExpression = replacedReadExpression.Type == type
                ? replacedReadExpression
                : Expression.Convert(replacedReadExpression, type);

            return Expression.Block(
                variables: new[] { valueBufferVariable },
                Expression.Assign(valueBufferVariable, singleResult),
                Expression.Condition(
                    Expression.MakeMemberAccess(valueBufferVariable, _valueBufferIsEmpty),
                    Expression.Default(type),
                    replacedReadExpression));
        }

        [UsedImplicitly]
        private static T GetParameterValue<T>(QueryContext queryContext, string parameterName)
            => (T)queryContext.ParameterValues[parameterName];

        private static bool IsConvertedToNullable(Expression result, Expression original)
            => result.Type.IsNullableType()
                && !original.Type.IsNullableType()
                && result.Type.UnwrapNullableType() == original.Type;

        private static Expression ConvertToNullable(Expression expression)
            => !expression.Type.IsNullableType()
                ? Expression.Convert(expression, expression.Type.MakeNullable())
                : expression;

        private static Expression ConvertToNonNullable(Expression expression)
            => expression.Type.IsNullableType()
                ? Expression.Convert(expression, expression.Type.UnwrapNullableType())
                : expression;

        private IProperty FindProperty(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert
                && expression.Type.IsNullableType()
                && expression is UnaryExpression unaryExpression
                && expression.Type.UnwrapNullableType() == unaryExpression.Type)
            {
                expression = unaryExpression.Operand;
            }

            if (expression is MethodCallExpression readValueMethodCall
                && readValueMethodCall.Method.IsGenericMethod
                && readValueMethodCall.Method.GetGenericMethodDefinition() == ExpressionExtensions.ValueBufferTryReadValueMethod)
            {
                return (IProperty)((ConstantExpression)readValueMethodCall.Arguments[2]).Value;
            }

            return null;
        }

        private bool TryRewriteContainsEntity(Expression source, Expression item, out Expression result)
        {
            result = null;

            if (!(item is EntityReferenceExpression itemEntityReference))
            {
                return false;
            }

            var entityType = itemEntityReference.EntityType;
            var primaryKeyProperties = entityType.FindPrimaryKey()?.Properties;
            if (primaryKeyProperties == null)
            {
                throw new InvalidOperationException(CoreStrings.EntityEqualityOnKeylessEntityNotSupported(
                    nameof(Queryable.Contains), entityType.DisplayName()));
            }

            if (primaryKeyProperties.Count > 1)
            {
                throw new InvalidOperationException(
                    CoreStrings.EntityEqualityOnCompositeKeyEntitySubqueryNotSupported(nameof(Queryable.Contains), entityType.DisplayName()));
            }

            var property = primaryKeyProperties[0];
            Expression rewrittenSource;
            switch (source)
            {
                case ConstantExpression constantExpression:
                    var values = (IEnumerable)constantExpression.Value;
                    var propertyValueList =
                        (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(property.ClrType.MakeNullable()));
                    var propertyGetter = property.GetGetter();
                    foreach (var value in values)
                    {
                        propertyValueList.Add(propertyGetter.GetClrValue(value));
                    }

                    rewrittenSource = Expression.Constant(propertyValueList);
                    break;

                case MethodCallExpression methodCallExpression
                    when methodCallExpression.Method.IsGenericMethod
                    && methodCallExpression.Method.GetGenericMethodDefinition() == _getParameterValueMethodInfo:
                    var parameterName = (string)((ConstantExpression)methodCallExpression.Arguments[1]).Value;
                    var lambda = Expression.Lambda(
                        Expression.Call(
                            _parameterListValueExtractorMethodInfo.MakeGenericMethod(entityType.ClrType, property.ClrType.MakeNullable()),
                            QueryCompilationContext.QueryContextParameter,
                            Expression.Constant(parameterName, typeof(string)),
                            Expression.Constant(property, typeof(IProperty))),
                        QueryCompilationContext.QueryContextParameter
                    );

                    var newParameterName =
                        $"{_runtimeParameterPrefix}"
                        + $"{parameterName.Substring(QueryCompilationContext.QueryParameterPrefix.Length)}_{property.Name}";

                    rewrittenSource = _queryCompilationContext.RegisterRuntimeParameter(newParameterName, lambda);
                    break;

                default:
                    return false;
            }

            result = Visit(
                Expression.Call(
                    EnumerableMethods.Contains.MakeGenericMethod(property.ClrType.MakeNullable()),
                    rewrittenSource,
                    CreatePropertyAccessExpression(item, property)));

            return true;
        }

        private bool TryRewriteEntityEquality(ExpressionType nodeType, Expression left, Expression right, bool equalsMethod, out Expression result)
        {
            var leftEntityReference = left as EntityReferenceExpression;
            var rightEntityReference = right as EntityReferenceExpression;

            if (leftEntityReference == null
                && rightEntityReference == null)
            {
                result = null;
                return false;
            }

            if (IsNullConstantExpression(left)
                || IsNullConstantExpression(right))
            {
                var nonNullEntityReference = IsNullConstantExpression(left) ? rightEntityReference : leftEntityReference;
                // ReSharper disable once PossibleNullReferenceException
                var entityType1 = nonNullEntityReference.EntityType;
                var primaryKeyProperties1 = entityType1.FindPrimaryKey()?.Properties;
                if (primaryKeyProperties1 == null)
                {
                    throw new InvalidOperationException(CoreStrings.EntityEqualityOnKeylessEntityNotSupported(
                        GetOperator(nodeType, equalsMethod),
                        entityType1.DisplayName()));
                }

                result = Visit(
                    primaryKeyProperties1.Select(
                            p =>
                                Expression.MakeBinary(
                                    nodeType, CreatePropertyAccessExpression(nonNullEntityReference, p),
                                    Expression.Constant(null, p.ClrType.MakeNullable())))
                        .Aggregate((l, r) => nodeType == ExpressionType.Equal ? Expression.OrElse(l, r) : Expression.AndAlso(l, r)));

                return true;
            }

            var leftEntityType = leftEntityReference?.EntityType;
            var rightEntityType = rightEntityReference?.EntityType;
            var entityType = leftEntityType ?? rightEntityType;

            Debug.Assert(entityType != null, "At least either side should be entityReference so entityType should be non-null.");

            if (leftEntityType != null
                && rightEntityType != null
                && leftEntityType.GetRootType() != rightEntityType.GetRootType())
            {
                result = Expression.Constant(false);
                return true;
            }

            // Sonar provides this warning: However entityType cannot be null during a compile debug due to the Debug.Assert above.
            //
#pragma warning disable S2259
            var primaryKeyProperties = entityType.FindPrimaryKey()?.Properties;
#pragma warning restore
            if (primaryKeyProperties == null)
            {
                throw new InvalidOperationException(CoreStrings.EntityEqualityOnKeylessEntityNotSupported(
                    GetOperator(nodeType, equalsMethod),
                    entityType.DisplayName()));
            }

            if (primaryKeyProperties.Count > 1
                && (leftEntityReference?.SubqueryEntity != null
                    || rightEntityReference?.SubqueryEntity != null))
            {
                throw new InvalidOperationException(CoreStrings.EntityEqualityOnCompositeKeyEntitySubqueryNotSupported(
                    GetOperator(nodeType, equalsMethod),
                    entityType.DisplayName()));
            }

            result = Visit(
                primaryKeyProperties.Select(
                        p =>
                            Expression.MakeBinary(
                                nodeType,
                                CreatePropertyAccessExpression(left, p),
                                CreatePropertyAccessExpression(right, p)))
                    .Aggregate((l, r) => Expression.AndAlso(l, r)));

            return true;
        }

        private string GetOperator(ExpressionType nodeType, bool equalsMethod)
        {
            if (nodeType == ExpressionType.Equal)
            {
                return equalsMethod ? nameof(object.Equals) : "==";
            }

            return equalsMethod ? "!" + nameof(object.Equals) : "!=";
        }

        private Expression CreatePropertyAccessExpression(Expression target, IProperty property)
        {
            switch (target)
            {
                case ConstantExpression constantExpression:
                    return Expression.Constant(
                        property.GetGetter().GetClrValue(constantExpression.Value), property.ClrType.MakeNullable());

                case MethodCallExpression methodCallExpression
                    when methodCallExpression.Method.IsGenericMethod
                    && methodCallExpression.Method.GetGenericMethodDefinition() == _getParameterValueMethodInfo:
                    var parameterName = (string)((ConstantExpression)methodCallExpression.Arguments[1]).Value;
                    var lambda = Expression.Lambda(
                        Expression.Call(
                            _parameterValueExtractorMethodInfo.MakeGenericMethod(property.ClrType.MakeNullable()),
                            QueryCompilationContext.QueryContextParameter,
                            Expression.Constant(parameterName, typeof(string)),
                            Expression.Constant(property, typeof(IProperty))),
                        QueryCompilationContext.QueryContextParameter);

                    var newParameterName =
                        $"{_runtimeParameterPrefix}"
                        + $"{parameterName.Substring(QueryCompilationContext.QueryParameterPrefix.Length)}_{property.Name}";

                    return _queryCompilationContext.RegisterRuntimeParameter(newParameterName, lambda);

                case MemberInitExpression memberInitExpression
                    when memberInitExpression.Bindings.SingleOrDefault(
                        mb => mb.Member.Name == property.Name) is MemberAssignment memberAssignment:
                    return memberAssignment.Expression.Type.IsNullableType()
                        ? memberAssignment.Expression
                        : Expression.Convert(memberAssignment.Expression, property.ClrType.MakeNullable());

                case NewExpression newExpression
                    when CanEvaluate(newExpression):
                    return CreatePropertyAccessExpression(GetValue(newExpression), property);

                case MemberInitExpression memberInitExpression
                    when CanEvaluate(memberInitExpression):
                    return CreatePropertyAccessExpression(GetValue(memberInitExpression), property);

                default:
                    return target.CreateEFPropertyExpression(property);
            }
        }

        private static T ParameterValueExtractor<T>(QueryContext context, string baseParameterName, IProperty property)
        {
            var baseParameter = context.ParameterValues[baseParameterName];
            return baseParameter == null ? (T)(object)null : (T)property.GetGetter().GetClrValue(baseParameter);
        }

        private static List<TProperty> ParameterListValueExtractor<TEntity, TProperty>(
            QueryContext context,
            string baseParameterName,
            IProperty property)
        {
            if (!(context.ParameterValues[baseParameterName] is IEnumerable<TEntity> baseListParameter))
            {
                return null;
            }

            var getter = property.GetGetter();
            return baseListParameter.Select(e => e != null ? (TProperty)getter.GetClrValue(e) : (TProperty)(object)null).ToList();
        }

        private static ConstantExpression GetValue(Expression expression)
            => Expression.Constant(
                Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object))).Compile().Invoke(),
                expression.Type);

        private static bool CanEvaluate(Expression expression)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (expression)
#pragma warning restore IDE0066 // Convert switch statement to expression
            {
                case ConstantExpression:
                    return true;

                case NewExpression newExpression:
                    return newExpression.Arguments.All(e => CanEvaluate(e));

                case MemberInitExpression memberInitExpression:
                    return CanEvaluate(memberInitExpression.NewExpression)
                        && memberInitExpression.Bindings.All(
                            mb => mb is MemberAssignment memberAssignment && CanEvaluate(memberAssignment.Expression));

                default:
                    return false;
            }
        }

        private static Expression ConvertObjectArrayEqualityComparison(Expression left, Expression right)
        {
            var leftExpressions = ((NewArrayExpression)left).Expressions;
            var rightExpressions = ((NewArrayExpression)right).Expressions;

            return leftExpressions.Zip(
                    rightExpressions,
                    (l, r) =>
                    {
                        l = RemoveObjectConvert(l);
                        r = RemoveObjectConvert(r);
                        if (l.Type.IsNullableType())
                        {
                            r = r.Type.IsNullableType() ? r : Expression.Convert(r, l.Type);
                        }
                        else if (r.Type.IsNullableType())
                        {
                            l = l.Type.IsNullableType() ? l : Expression.Convert(l, r.Type);
                        }

                        return Expression.Equal(l, r);
                    })
                .Aggregate((a, b) => Expression.AndAlso(a, b));

            static Expression RemoveObjectConvert(Expression expression)
                => expression is UnaryExpression unaryExpression
                    && expression.Type == typeof(object)
                    && expression.NodeType == ExpressionType.Convert
                        ? unaryExpression.Operand
                        : expression;
        }

        private static bool IsNullConstantExpression(Expression expression)
            => expression is ConstantExpression constantExpression && constantExpression.Value == null;

        [DebuggerStepThrough]
        private static bool TranslationFailed(Expression original, Expression translation)
            => original != null && (translation == null || translation is EntityReferenceExpression);

        private static bool TranslationFailed(Expression translation)
            => translation == null || translation is EntityReferenceExpression;

        private static bool UbigiaLike(string matchExpression, string pattern, string escapeCharacter)
        {
            //TODO: this fixes https://github.com/aspnet/EntityFramework/issues/8656 by insisting that
            // the "escape character" is a string but just using the first character of that string,
            // but we may later want to allow the complete string as the "escape character"
            // in which case we need to change the way we construct the regex below.
            var singleEscapeCharacter =
                (escapeCharacter == null || escapeCharacter.Length == 0)
                    ? (char?)null
                    : escapeCharacter.First();

            if (matchExpression == null
                || pattern == null)
            {
                return false;
            }

            if (matchExpression.Equals(pattern, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (matchExpression.Length == 0
                || pattern.Length == 0)
            {
                return false;
            }

            var escapeRegexCharsPattern
                = singleEscapeCharacter == null
                    ? _defaultEscapeRegexCharsPattern
                    : BuildEscapeRegexCharsPattern(_regexSpecialChars.Where(c => c != singleEscapeCharacter));

            var regexPattern
                = Regex.Replace(
                    pattern,
                    escapeRegexCharsPattern,
                    c => @"\" + c,
                    default,
                    _regexTimeout);

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < regexPattern.Length; i++)
            {
                var c = regexPattern[i];
                var escaped = i > 0 && regexPattern[i - 1] == singleEscapeCharacter;

                switch (c)
                {
                    case '_':
                    {
                        stringBuilder.Append(escaped ? '_' : '.');
                        break;
                    }
                    case '%':
                    {
                        stringBuilder.Append(escaped ? "%" : ".*");
                        break;
                    }
                    default:
                    {
                        if (c != singleEscapeCharacter)
                        {
                            stringBuilder.Append(c);
                        }

                        break;
                    }
                }
            }

            regexPattern = stringBuilder.ToString();

            return Regex.IsMatch(
                matchExpression,
                @"\A" + regexPattern + @"\s*\z",
                RegexOptions.IgnoreCase | RegexOptions.Singleline,
                _regexTimeout);
        }

        private sealed class EntityReferenceFindingExpressionVisitor : ExpressionVisitor
        {
            private bool _found;

            public bool Find(Expression expression)
            {
                _found = false;

                Visit(expression);

                return _found;
            }

            public override Expression Visit(Expression node)
            {
                if (_found)
                {
                    return node;
                }

                if (node is EntityReferenceExpression)
                {
                    _found = true;
                    return node;
                }

                return base.Visit(node);
            }
        }

        private sealed class EntityReferenceExpression : Expression
        {
            public EntityReferenceExpression(EntityShaperExpression parameter)
            {
                ParameterEntity = parameter;
                EntityType = parameter.EntityType;
            }

            public EntityReferenceExpression(ShapedQueryExpression subquery)
            {
                SubqueryEntity = subquery;
                EntityType = ((EntityShaperExpression)subquery.ShaperExpression).EntityType;
            }

            private EntityReferenceExpression(EntityReferenceExpression entityReferenceExpression, IEntityType entityType)
            {
                ParameterEntity = entityReferenceExpression.ParameterEntity;
                SubqueryEntity = entityReferenceExpression.SubqueryEntity;
                EntityType = entityType;
            }

            public EntityShaperExpression ParameterEntity { get; }
            public ShapedQueryExpression SubqueryEntity { get; }
            public IEntityType EntityType { get; }

            public override Type Type
                => EntityType.ClrType;

            public override ExpressionType NodeType
                => ExpressionType.Extension;

            public Expression Convert(Type type)
            {
                if (type == typeof(object) // Ignore object conversion
                    || type.IsAssignableFrom(Type)) // Ignore casting to base type/interface
                {
                    return this;
                }

                var derivedEntityType = EntityType.GetDerivedTypes().FirstOrDefault(et => et.ClrType == type);

                return derivedEntityType == null ? null : new EntityReferenceExpression(this, derivedEntityType);
            }
        }

        private sealed class GroupingElementExpression : Expression
        {
            public GroupingElementExpression(Expression source, Expression selector, ParameterExpression valueBufferParameter)
            {
                Source = source;
                ValueBufferParameter = valueBufferParameter;
                Selector = selector;
            }

            public Expression Source { get; private set; }
            public bool IsDistinct { get; private set; }
            public Expression Selector { get; private set; }
            public ParameterExpression ValueBufferParameter { get; }

            public GroupingElementExpression ApplyDistinct()
            {
                IsDistinct = true;

                return this;
            }

            public GroupingElementExpression ApplySelector(Expression expression)
            {
                Selector = expression;

                return this;
            }

            public GroupingElementExpression UpdateSource(Expression source)
            {
                Source = source;

                return this;
            }

            public override Type Type
                => typeof(IEnumerable<>).MakeGenericType(Selector.Type);

            public override ExpressionType NodeType
                => ExpressionType.Extension;
        }
    }
}
