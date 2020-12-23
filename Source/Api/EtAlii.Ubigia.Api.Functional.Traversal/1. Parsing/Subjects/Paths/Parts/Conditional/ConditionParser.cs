namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using Moppet.Lapa;

    internal class ConditionParser : IConditionParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IDateTimeValueParser _dateTimeValueParser;
        private readonly ITimeSpanValueParser _timeSpanValueParser;
        private readonly IBooleanValueParser _booleanValueParser;
        private readonly IIntegerValueParser _integerValueParser;
        private readonly IFloatValueParser _floatValueParser;

        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = nameof(Condition);

        private const string _propertyId = "Property";
        private const string _conditionId = "ConditionType";
        private const string _valueId = "Value";

        private readonly Func<LpNode, LpNode>[] _innerValueFinders;

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

            var conditionType = condition switch
            {
                "=" => ConditionType.Equal,
                "!=" => ConditionType.NotEqual,
                "<" => ConditionType.LessThan,
                "<=" => ConditionType.LessThanOrEqual,
                ">" => ConditionType.MoreThan,
                ">=" => ConditionType.MoreThanOrEqual,
                _ => throw new ScriptParserException("Unable to parse condition: " + condition ?? "<NULL>")
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
                    value = innerValueNode switch
                    {
                        { } node when node.Id == _quotedTextParser.Id => _quotedTextParser.Parse(node),
                        { } node when node.Id == _dateTimeValueParser.Id => _dateTimeValueParser.Parse(node),
                        { } node when node.Id == _timeSpanValueParser.Id => _timeSpanValueParser.Parse(node),
                        { } node when node.Id == _booleanValueParser.Id => _booleanValueParser.Parse(node),
                        { } node when node.Id == _integerValueParser.Id => _integerValueParser.Parse(node),
                        { } node when node.Id == _floatValueParser.Id => _floatValueParser.Parse(node),
                        _ => throw new NotSupportedException($"Cannot find condition parser for: {innerValueNode.ToString() ?? "NULL"}")
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
