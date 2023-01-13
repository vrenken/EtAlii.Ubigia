// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Collections.Generic;
using Moppet.Lapa;

internal abstract class KeyValuePairParserBase
{
    private readonly INodeValidator _nodeValidator;
    protected readonly IQuotedTextParser QuotedTextParser;
    private readonly IDateTimeValueParser _dateTimeValueParser;
    private readonly ITimeSpanValueParser _timeSpanValueParser;
    private readonly IBooleanValueParser _booleanValueParser;
    private readonly IIntegerValueParser _integerValueParser;
    private readonly IFloatValueParser _floatValueParser;
    protected readonly IWhitespaceParser WhitespaceParser;

    private readonly INodeFinder _nodeFinder;

    public LpsParser Parser => _parser.Value;
    private readonly Lazy<LpsParser> _parser;

    public const string Id = "KeyValuePair";

    private readonly Func<LpNode, LpNode>[] _innerValueFinders;
    protected readonly LpsAlternatives TypeParsers;

    protected const string KeyId = "Key";
    protected const string ValueId = "Value";

    // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
    // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
    // specified by SonarQube. The current setup here is already some kind of facade that hides away many of the type specific parsing. Therefore refactoring to facades won't work.
    // Therefore this pragma warning disable of S107.
#pragma warning disable S107
    protected KeyValuePairParserBase(
        INodeValidator nodeValidator,
        IQuotedTextParser quotedTextParser,
        IDateTimeValueParser dateTimeValueParser,
        ITimeSpanValueParser timeSpanValueParser,
        IBooleanValueParser booleanValueParser,
        IIntegerValueParser integerValueParser,
        IFloatValueParser floatValueParser,
        IWhitespaceParser whitespaceParser,
        INodeFinder nodeFinder)
#pragma warning restore S107
    {
        _nodeValidator = nodeValidator;
        QuotedTextParser = quotedTextParser;
        _dateTimeValueParser = dateTimeValueParser;
        _timeSpanValueParser = timeSpanValueParser;
        _booleanValueParser = booleanValueParser;
        _integerValueParser = integerValueParser;
        _floatValueParser = floatValueParser;
        WhitespaceParser = whitespaceParser;
        _nodeFinder = nodeFinder;

        TypeParsers =
        (
            QuotedTextParser.Parser |
            dateTimeValueParser.Parser |
            timeSpanValueParser.Parser |
            booleanValueParser.Parser |
            floatValueParser.Parser |
            integerValueParser.Parser
        );

        _parser = new Lazy<LpsParser>(InitializeParser);

        _innerValueFinders = new Func<LpNode, LpNode>[]
        {
            node => _nodeFinder.FindFirst(node, QuotedTextParser.Id),
            node => _nodeFinder.FindFirst(node, dateTimeValueParser.Id),
            node => _nodeFinder.FindFirst(node, timeSpanValueParser.Id),
            node => _nodeFinder.FindFirst(node, booleanValueParser.Id),
            node => _nodeFinder.FindFirst(node, integerValueParser.Id),
            node => _nodeFinder.FindFirst(node, floatValueParser.Id),

        };
    }

    protected abstract LpsParser InitializeParser();


    public KeyValuePair<string, object> Parse(LpNode node)
    {
        _nodeValidator.EnsureSuccess(node, Id);

        var keyNode = _nodeFinder.FindFirst(node, KeyId);
        var constantNode = keyNode.FirstOrDefault(n => n.Id == QuotedTextParser.Id);
        var key = constantNode == null ? keyNode.Match.ToString() : QuotedTextParser.Parse(constantNode);

        var valueNode = _nodeFinder.FindFirst(node, ValueId);

        object value = null;

        if (valueNode != null)
        {
            value = Determine(valueNode);
        }

        return new KeyValuePair<string, object>(key, value);
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
                    Traversal.QuotedTextParser.Id => QuotedTextParser.Parse(innerValueNode),
                    DateTimeValueParser.Id => _dateTimeValueParser.Parse(innerValueNode),
                    TimeSpanValueParser.Id => _timeSpanValueParser.Parse(innerValueNode),
                    BooleanValueParser.Id => _booleanValueParser.Parse(innerValueNode),
                    IntegerValueParser.Id => _integerValueParser.Parse(innerValueNode),
                    FloatValueParser.Id => _floatValueParser.Parse(innerValueNode),
                    _ => throw new InvalidOperationException($"Unable to find parser for KeyValuePair: {innerValueNode.Id}")
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
