﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using Moppet.Lapa;

internal sealed class ConditionParser : IConditionParser
{
    private readonly IQuotedTextParser _quotedTextParser;
    private readonly IDateTimeValueParser _dateTimeValueParser;
    private readonly ITimeSpanValueParser _timeSpanValueParser;
    private readonly IBooleanValueParser _booleanValueParser;
    private readonly IIntegerValueParser _integerValueParser;
    private readonly IFloatValueParser _floatValueParser;

    private readonly INodeFinder _nodeFinder;

    public LpsParser Parser { get; }

    public string Id => nameof(Condition);

    private const string PropertyId = "Property";
    private const string ConditionId = "ConditionType";
    private const string ValueId = "Value";

    private readonly Func<LpNode, LpNode>[] _innerValueFinders;

    public ConditionParser(
        IQuotedTextParser quotedTextParser,
        IDateTimeValueParser dateTimeValueParser,
        ITimeSpanValueParser timeSpanValueParser,
        IBooleanValueParser booleanValueParser,
        IIntegerValueParser integerValueParser,
        IFloatValueParser floatValueParser,
        INodeFinder nodeFinder)
    {
        _quotedTextParser = quotedTextParser;
        _dateTimeValueParser = dateTimeValueParser;
        _timeSpanValueParser = timeSpanValueParser;
        _booleanValueParser = booleanValueParser;
        _integerValueParser = integerValueParser;
        _floatValueParser = floatValueParser;
        _nodeFinder = nodeFinder;

        var typeParsers =
        (
            quotedTextParser.Parser |
            dateTimeValueParser.Parser |
            timeSpanValueParser.Parser |
            booleanValueParser.Parser |
            floatValueParser.Parser |
            integerValueParser.Parser
        );

        var conditions =
        (
            (Lp.Char('!').Maybe() + Lp.Char('=')) |
            (Lp.Char('>') + Lp.Char('=').Maybe()) |
            (Lp.Char('<') + Lp.Char('=').Maybe())
        ).Id(ConditionId);

        Parser = new LpsParser(Id, true,
            (
                Lp.Name().Id(PropertyId) |
                Lp.Char('"') + Lp.Name().Id(PropertyId) + Lp.Char('"') |
                Lp.Char('\'') + Lp.Name().Id(PropertyId) + Lp.Char('\'')
            ) +
            Lp.OneOrMore(' ').Maybe() +
            conditions +
            Lp.OneOrMore(' ').Maybe() +
            new LpsParser(ValueId, true, typeParsers).Maybe());

        _innerValueFinders = new Func<LpNode, LpNode>[]
        {
            node => _nodeFinder.FindFirst(node, quotedTextParser.Id),
            node => _nodeFinder.FindFirst(node, dateTimeValueParser.Id),
            node => _nodeFinder.FindFirst(node, timeSpanValueParser.Id),
            node => _nodeFinder.FindFirst(node, booleanValueParser.Id),
            node => _nodeFinder.FindFirst(node, integerValueParser.Id),
            node => _nodeFinder.FindFirst(node, floatValueParser.Id),

        };
    }

    public Condition Parse(LpNode node, INodeValidator nodeValidator)
    {
        nodeValidator.EnsureSuccess(node, Id);

        var propertyNode = _nodeFinder.FindFirst(node, PropertyId);
        var property = propertyNode.Match.ToString();

        var valueNode = _nodeFinder.FindFirst(node, ValueId);
        object value = null;

        if (valueNode != null)
        {
            value = Determine(valueNode);
        }

        var conditionNode = _nodeFinder.FindFirst(node, ConditionId);
        var condition = conditionNode.Match.ToString();

        var conditionType = condition switch
        {
            "="  => ConditionType.Equal,
            "!=" => ConditionType.NotEqual,
            "<"  => ConditionType.LessThan,
            "<=" => ConditionType.LessThanOrEqual,
            ">"  => ConditionType.MoreThan,
            ">=" => ConditionType.MoreThanOrEqual,
            _    => throw new ScriptParserException("Unable to parse condition: " + condition)
        };
        return new Condition(property, conditionType, value);
    }

    private object Determine(LpNode valueNode)
    {
        object value = null;

        foreach (var innerValueFinder in _innerValueFinders)
        {
            var innerValueNode = innerValueFinder(valueNode);
            if (innerValueNode != null)
            {
                value = innerValueNode.Id switch
                {
                    QuotedTextParser.Id => _quotedTextParser.Parse(innerValueNode),
                    DateTimeValueParser.Id => _dateTimeValueParser.Parse(innerValueNode),
                    TimeSpanValueParser.Id => _timeSpanValueParser.Parse(innerValueNode),
                    BooleanValueParser.Id => _booleanValueParser.Parse(innerValueNode),
                    IntegerValueParser.Id => _integerValueParser.Parse(innerValueNode),
                    FloatValueParser.Id => _floatValueParser.Parse(innerValueNode),
                    _ => throw new NotSupportedException($"Cannot find value in: {innerValueNode.Match}")
                };
                break;
            }
        }

        return value;
    }

    public bool CanParse(LpNode node)
    {
        return node.Id == Id;
    }
}
