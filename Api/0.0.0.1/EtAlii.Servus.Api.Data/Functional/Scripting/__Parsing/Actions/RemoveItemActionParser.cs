namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class RemoveItemActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public RemoveItemActionParser(
            IParserHelper parserHelper, 
            VariableParser variableParser,
            PathParser pathParser,
            TerminalExpressions terminalExpressions,
            OperatorExpressions operatorExpressions, 
            PathExpressions pathExpressions)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _pathParser = pathParser;

            _actionId = NonTerminalId.RemoveAction;

            // /[PATH]/ -= [SYMBOL] #[COMMENT]
            _expressionParser = (pathExpressions.Rooted + terminalExpressions.Separator + operatorExpressions.Remove + terminalExpressions.Symbol).Id(NonTerminalId.RemoveAction);
        }

        public Action Parse(LpNode node)
        {
            var path = _pathParser.Parse(node, 0);
            var symbolNode = _parserHelper.FindFirst(node.Children.ElementAt(1), TerminalId.Symbol);
            var itemName = symbolNode.Match.ToString();
            var result = new RemoveItem(path, itemName);
            return result;
        }
    }
}
