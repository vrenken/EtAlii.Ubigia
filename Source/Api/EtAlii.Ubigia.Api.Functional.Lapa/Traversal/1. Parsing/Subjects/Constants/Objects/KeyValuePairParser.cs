// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using Moppet.Lapa;

    internal class KeyValuePairParser : IKeyValuePairParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IDateTimeValueParser _dateTimeValueParser;
        private readonly ITimeSpanValueParser _timeSpanValueParser;
        private readonly IBooleanValueParser _booleanValueParser;
        private readonly IIntegerValueParser _integerValueParser;
        private readonly IFloatValueParser _floatValueParser;
        private readonly IWhitespaceParser _whitespaceParser;

        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; private set; }

        public const string Id = "KeyValuePair";
        string IKeyValuePairParser.Id => Id;

        private const string KeyId = "Key";
        private const string ValueId = "Value";

        private readonly Func<LpNode, LpNode>[] _innerValueFinders;
        private readonly LpsAlternatives _typeParsers;

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many of the type specific parsing. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
        public KeyValuePairParser(
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
            _quotedTextParser = quotedTextParser;
            _dateTimeValueParser = dateTimeValueParser;
            _timeSpanValueParser = timeSpanValueParser;
            _booleanValueParser = booleanValueParser;
            _integerValueParser = integerValueParser;
            _floatValueParser = floatValueParser;
            _whitespaceParser = whitespaceParser;
            _nodeFinder = nodeFinder;

            _typeParsers =
                (
                    _quotedTextParser.Parser |
                    dateTimeValueParser.Parser |
                    timeSpanValueParser.Parser |
                    booleanValueParser.Parser |
                    floatValueParser.Parser |
                    integerValueParser.Parser
                );

            Initialize();

            _innerValueFinders = new Func<LpNode, LpNode>[]
            {
                node => _nodeFinder.FindFirst(node, _quotedTextParser.Id),
                node => _nodeFinder.FindFirst(node, dateTimeValueParser.Id),
                node => _nodeFinder.FindFirst(node, timeSpanValueParser.Id),
                node => _nodeFinder.FindFirst(node, booleanValueParser.Id),
                node => _nodeFinder.FindFirst(node, integerValueParser.Id),
                node => _nodeFinder.FindFirst(node, floatValueParser.Id),

            };
        }

        public void Initialize(LpsParser separator = null)
        {
            var defaultSeparator = _whitespaceParser.Optional + Lp.Char(':') + _whitespaceParser.Optional;
            separator ??= defaultSeparator;

            Parser = new LpsParser(Id, true,
                (
                    Lp.Name().Id(KeyId) |
                    _quotedTextParser.Parser.Wrap(KeyId)
                ) +
                separator +
                new LpsParser(ValueId, true, _typeParsers).Maybe());
        }

        public KeyValuePair<string, object> Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var keyNode = _nodeFinder.FindFirst(node, KeyId);
            var constantNode = keyNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var key = constantNode == null ? keyNode.Match.ToString() : _quotedTextParser.Parse(constantNode);

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
                        QuotedTextParser.Id => _quotedTextParser.Parse(innerValueNode),
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
}
