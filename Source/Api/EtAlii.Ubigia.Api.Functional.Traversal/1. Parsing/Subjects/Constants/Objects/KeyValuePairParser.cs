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
        string IKeyValuePairParser.Id { get; } = Id;

        private const string _keyId = "Key";
        private const string _valueId = "Value";

        private readonly Func<LpNode, LpNode>[] _innerValueFinders;
        private readonly LpsAlternatives _typeParsers;

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
                    _dateTimeValueParser.Parser |
                    _timeSpanValueParser.Parser |
                    _booleanValueParser.Parser |
                    _floatValueParser.Parser |
                    _integerValueParser.Parser
                );

            Initialize();

            _innerValueFinders = new Func<LpNode, LpNode>[]
            {
                node => _nodeFinder.FindFirst(node, _quotedTextParser.Id),
                node => _nodeFinder.FindFirst(node, _dateTimeValueParser.Id),
                node => _nodeFinder.FindFirst(node, _timeSpanValueParser.Id),
                node => _nodeFinder.FindFirst(node, _booleanValueParser.Id),
                node => _nodeFinder.FindFirst(node, _integerValueParser.Id),
                node => _nodeFinder.FindFirst(node, _floatValueParser.Id),

            };
        }

        public void Initialize(LpsParser separator = null)
        {
            var defaultSeparator = _whitespaceParser.Optional + Lp.Char(':') + _whitespaceParser.Optional;
            separator ??= defaultSeparator;

            Parser = new LpsParser(Id, true,
                (
                    Lp.Name().Id(_keyId) |
                    _quotedTextParser.Parser.Wrap(_keyId)
                ) +
                separator +
                new LpsParser(_valueId, true, _typeParsers).Maybe());
        }

        public KeyValuePair<string, object> Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var keyNode = _nodeFinder.FindFirst(node, _keyId);
            var constantNode = keyNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var key = constantNode == null ? keyNode.Match.ToString() : _quotedTextParser.Parse(constantNode);

            var valueNode = _nodeFinder.FindFirst(node, _valueId);

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
                    value = innerValueNode switch
                    {
                        { } node when node.Id == _quotedTextParser.Id => _quotedTextParser.Parse(node),
                        { } node when node.Id == _dateTimeValueParser.Id => _dateTimeValueParser.Parse(node),
                        { } node when node.Id == _timeSpanValueParser.Id => _timeSpanValueParser.Parse(node),
                        { } node when node.Id == _booleanValueParser.Id => _booleanValueParser.Parse(node),
                        { } node when node.Id == _integerValueParser.Id => _integerValueParser.Parse(node),
                        { } node when node.Id == _floatValueParser.Id => _floatValueParser.Parse(node),
                        _ => throw new NotSupportedException($"Cannot find key-value parser for: {innerValueNode.ToString() ?? "NULL"}")
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
