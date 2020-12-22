namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class ConditionParser : IConditionParser
    {
        private readonly INodeValidator _nodeValidator;

        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = nameof(Condition);

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
                ).Id(_conditionId);

            Parser = new LpsParser(Id, true,
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
                node => _nodeFinder.FindFirst(node, quotedTextParser.Id),
                node => _nodeFinder.FindFirst(node, dateTimeValueParser.Id),
                node => _nodeFinder.FindFirst(node, timeSpanValueParser.Id),
                node => _nodeFinder.FindFirst(node, booleanValueParser.Id),
                node => _nodeFinder.FindFirst(node, integerValueParser.Id),
                node => _nodeFinder.FindFirst(node, floatValueParser.Id),

            };

            _valueParserSelector = new Selector<LpNode, Func<LpNode, object>>()
                .Register(node => node.Id == quotedTextParser.Id, node => quotedTextParser.Parse(node))
                .Register(node => node.Id == dateTimeValueParser.Id, node => dateTimeValueParser.Parse(node))
                .Register(node => node.Id == timeSpanValueParser.Id, node => timeSpanValueParser.Parse(node))
                .Register(node => node.Id == booleanValueParser.Id, node => booleanValueParser.Parse(node))
                .Register(node => node.Id == integerValueParser.Id, node => integerValueParser.Parse(node))
                .Register(node => node.Id == floatValueParser.Id, node => floatValueParser.Parse(node));
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
