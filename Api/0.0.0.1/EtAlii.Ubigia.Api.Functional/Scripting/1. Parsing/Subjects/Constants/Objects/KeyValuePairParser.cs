namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class KeyValuePairParser : IKeyValuePairParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IBooleanValueParser _booleanValueParser;
        private readonly IIntegerValueParser _integerValueParser;
        private readonly IFloatValueParser _floatValueParser;
        private readonly IDateTimeValueParser _dateTimeValueParser;
        private readonly ITimeSpanValueParser _timeSpanValueParser;

        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        public string Id => _id;
        private const string _id = "KeyValuePair";

        private const string _keyId = "Key";
        private const string _valueId = "Value";

        private readonly Func<LpNode, LpNode>[] _innerValueFinders;
        private readonly ISelector<LpNode, Func<LpNode, object>> _valueParserSelector; 

        public KeyValuePairParser(
            INodeValidator nodeValidator,
            IQuotedTextParser quotedTextParser,
            IDateTimeValueParser dateTimeValueParser,
            ITimeSpanValueParser timeSpanValueParser,
            IBooleanValueParser booleanValueParser,
            IIntegerValueParser integerValueParser,
            IFloatValueParser floatValueParser,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _quotedTextParser = quotedTextParser;
            _booleanValueParser = booleanValueParser;
            _integerValueParser = integerValueParser;
            _floatValueParser = floatValueParser;
            _dateTimeValueParser = dateTimeValueParser;
            _timeSpanValueParser = timeSpanValueParser;
            _nodeFinder = nodeFinder;

            var typeParsers = 
                (
                    _quotedTextParser.Parser | 
                    _dateTimeValueParser.Parser | 
                    _timeSpanValueParser.Parser |
                    _booleanValueParser.Parser | 
                    _floatValueParser.Parser | 
                    _integerValueParser.Parser
                );

            _parser = new LpsParser(Id, true,
                (
                    Lp.Name().Id(_keyId) |
                    _quotedTextParser.Parser.Wrap(_keyId)
                ) +
                Lp.OneOrMore(' ').Maybe() +
                Lp.Char(':') +
                Lp.OneOrMore(' ').Maybe() +
                new LpsParser(_valueId, true, typeParsers).Maybe());

            _innerValueFinders = new Func<LpNode, LpNode>[]
            {
                node => _nodeFinder.FindFirst(node, _quotedTextParser.Id),
                node => _nodeFinder.FindFirst(node, _dateTimeValueParser.Id),
                node => _nodeFinder.FindFirst(node, _timeSpanValueParser.Id),
                node => _nodeFinder.FindFirst(node, _booleanValueParser.Id),
                node => _nodeFinder.FindFirst(node, _integerValueParser.Id),
                node => _nodeFinder.FindFirst(node, _floatValueParser.Id),

            };

            _valueParserSelector = new Selector<LpNode, Func<LpNode, object>>()
                .Register(node => node.Id == _quotedTextParser.Id, node => _quotedTextParser.Parse(node))
                .Register(node => node.Id == _dateTimeValueParser.Id, node => _dateTimeValueParser.Parse(node))
                .Register(node => node.Id == _timeSpanValueParser.Id, node => _timeSpanValueParser.Parse(node))
                .Register(node => node.Id == _booleanValueParser.Id, node => _booleanValueParser.Parse(node))
                .Register(node => node.Id == _integerValueParser.Id, node => _integerValueParser.Parse(node))
                .Register(node => node.Id == _floatValueParser.Id, node => _floatValueParser.Parse(node));
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
                    var parser = _valueParserSelector.Select(innerValueNode);
                    value = parser(innerValueNode);
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
