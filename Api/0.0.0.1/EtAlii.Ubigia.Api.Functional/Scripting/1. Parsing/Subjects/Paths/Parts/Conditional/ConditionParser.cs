namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class ConditionParser : IConditionParser
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
        private const string _id = "Condition";

        private const string _propertyId = "Property";
        private const string _conditionId = "ConditionType";
        private const string _valueId = "Value";

        private readonly Func<LpNode, LpNode>[] _innerValueFinders;
        private readonly ISelector<LpNode, Func<LpNode, object>> _valueParserSelector;

        public ConditionParser(
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

            var conditions =
                (
                    (Lp.Char('!').Maybe() + Lp.Char('=')) |
                    (Lp.Char('>') + Lp.Char('=').Maybe()) |
                    (Lp.Char('<') + Lp.Char('=').Maybe()) 
                ).Id(_conditionId);

            _parser = new LpsParser(Id, true,
                (
                    Lp.Name().Id(_propertyId) |
                    Lp.Char('"') + Lp.Name().Id(_propertyId) + Lp.Char('"') |
                    Lp.Char('\'') + Lp.Name().Id(_propertyId) + Lp.Char('\'')
                ) +
                Lp.OneOrMore(' ').Maybe() +
                conditions +
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

        public Condition Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            
            var propertyNode = _nodeFinder.FindFirst(node, _propertyId);
            var property = propertyNode.Match.ToString();

            var valueNode = _nodeFinder.FindFirst(node, _valueId);
            object value = null;

            if (valueNode != null)
            {
                value = Determine(valueNode);
            }

            var conditionNode = _nodeFinder.FindFirst(node, _conditionId);
            var condition = conditionNode.Match.ToString();

            ConditionType conditionType;
            switch (condition)
            {
                case "=": conditionType = ConditionType.Equal; break;
                case "!=": conditionType = ConditionType.NotEqual; break;
                case "<": conditionType = ConditionType.LessThan; break;
                case "<=": conditionType = ConditionType.LessThanOrEqual; break;
                case ">": conditionType = ConditionType.MoreThan; break;
                case ">=": conditionType = ConditionType.MoreThanOrEqual; break;
                default:
                    throw new ScriptParserException("Unable to parse condition: " + condition ?? "<NULL>");
            }
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
