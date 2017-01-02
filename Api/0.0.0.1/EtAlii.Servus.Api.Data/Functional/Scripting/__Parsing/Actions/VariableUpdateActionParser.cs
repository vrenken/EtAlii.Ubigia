namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class VariableUpdateActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public VariableUpdateActionParser(
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

            _actionId = NonTerminalId.VariableUpdateAction;

            // ${VAR] <= /[PATH] <= $[VAR] #[COMMENT]
            _expressionParser = (terminalExpressions.Variable + operatorExpressions.Assign + pathExpressions.Rooted + operatorExpressions.Assign + terminalExpressions.Variable).Id(NonTerminalId.VariableUpdateAction);
        }

        public Action Parse(LpNode node)
        {
            var variableName = _variableParser.Parse(node, 0);
            var path = _pathParser.Parse(node, 2);
            var variableNode = _parserHelper.FindFirst(node.Children.ElementAt(4), TerminalId.Variable);
            var updateVariableName = variableNode.Match.ToString();
            var result = new VariableUpdateItem(path, variableName, updateVariableName);
            return result;
        }
    }
}
