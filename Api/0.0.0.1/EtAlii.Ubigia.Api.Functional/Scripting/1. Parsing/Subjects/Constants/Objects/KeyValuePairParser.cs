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

        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

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
            _nodeFinder = nodeFinder;

            var typeParsers = 
                (
                    _quotedTextParser.Parser | 
                    dateTimeValueParser.Parser | 
                    timeSpanValueParser.Parser |
                    booleanValueParser.Parser | 
                    floatValueParser.Parser | 
                    integerValueParser.Parser
                );

            Parser = new LpsParser(Id, true,
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
                node => _nodeFinder.FindFirst(node, dateTimeValueParser.Id),
                node => _nodeFinder.FindFirst(node, timeSpanValueParser.Id),
                node => _nodeFinder.FindFirst(node, booleanValueParser.Id),
                node => _nodeFinder.FindFirst(node, integerValueParser.Id),
                node => _nodeFinder.FindFirst(node, floatValueParser.Id),

            };

            _valueParserSelector = new Selector<LpNode, Func<LpNode, object>>()
                .Register(node => node.Id == _quotedTextParser.Id, node => _quotedTextParser.Parse(node))
                .Register(node => node.Id == dateTimeValueParser.Id, node => dateTimeValueParser.Parse(node))
                .Register(node => node.Id == timeSpanValueParser.Id, node => timeSpanValueParser.Parse(node))
                .Register(node => node.Id == booleanValueParser.Id, node => booleanValueParser.Parse(node))
                .Register(node => node.Id == integerValueParser.Id, node => integerValueParser.Parse(node))
                .Register(node => node.Id == floatValueParser.Id, node => floatValueParser.Parse(node));
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
