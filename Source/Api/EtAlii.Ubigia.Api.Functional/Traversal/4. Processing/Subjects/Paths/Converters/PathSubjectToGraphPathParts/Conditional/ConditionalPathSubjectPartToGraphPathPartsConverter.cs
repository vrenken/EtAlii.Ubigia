// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

internal class ConditionalPathSubjectPartToGraphPathPartsConverter : IConditionalPathSubjectPartToGraphPathPartsConverter
{
    private readonly IEqualPredicateFactory _equalPredicateFactory;
    private readonly INotEqualPredicateFactory _notEqualPredicateFactory;
    private readonly ILessThanPredicateFactory _lessThanPredicateFactory;
    private readonly ILessThanOrEqualPredicateFactory _lessThanOrEqualPredicateFactory;
    private readonly IMoreThanPredicateFactory _moreThanPredicateFactory;
    private readonly IMoreThanOrEqualPredicateFactory _moreThanOrEqualPredicateFactory;

    public ConditionalPathSubjectPartToGraphPathPartsConverter(
        IEqualPredicateFactory equalPredicateFactory,
        INotEqualPredicateFactory notEqualPredicateFactory,
        ILessThanPredicateFactory lessThanPredicateFactory,
        ILessThanOrEqualPredicateFactory lessThanOrEqualPredicateFactory,
        IMoreThanPredicateFactory moreThanPredicateFactory,
        IMoreThanOrEqualPredicateFactory moreThanOrEqualPredicateFactory)
    {
        _equalPredicateFactory = equalPredicateFactory;
        _notEqualPredicateFactory = notEqualPredicateFactory;
        _lessThanPredicateFactory = lessThanPredicateFactory;
        _lessThanOrEqualPredicateFactory = lessThanOrEqualPredicateFactory;
        _moreThanPredicateFactory = moreThanPredicateFactory;
        _moreThanOrEqualPredicateFactory = moreThanOrEqualPredicateFactory;
    }

    public Task<GraphPathPart[]> Convert(PathSubjectPart pathSubjectPart, int pathSubjectPartPosition, PathSubjectPart previousPathSubjectPart, PathSubjectPart nextPathSubjectPart, ExecutionScope scope)
    {
        var conditions = ((ConditionalPathSubjectPart) pathSubjectPart).Conditions;

        var result = conditions
            .Select(CreateGraphCondition)
            .Cast<GraphPathPart>()
            .ToArray();
        return Task.FromResult(result);
    }

    private GraphCondition CreateGraphCondition(Condition condition)
    {
        var predicate = condition.Type switch
        {
            { } type when type == ConditionType.Equal => _equalPredicateFactory.Create(condition),
            { } type when type == ConditionType.NotEqual => _notEqualPredicateFactory.Create(condition),
            { } type when type == ConditionType.LessThan => _lessThanPredicateFactory.Create(condition),
            { } type when type == ConditionType.LessThanOrEqual => _lessThanOrEqualPredicateFactory.Create(condition),
            { } type when type == ConditionType.MoreThan => _moreThanPredicateFactory.Create(condition),
            { } type when type == ConditionType.MoreThanOrEqual => _moreThanOrEqualPredicateFactory.Create(condition),
            _ => throw new NotSupportedException($"Cannot determine predicate for condition type {condition.Type.ToString() ?? "NULL"}")
        };

        var description = condition.ToString();
        return new GraphCondition(predicate, description);
    }
}
