namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class VariableStringAssignmentActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public VariableStringAssignmentActionParser(
            IParserHelper parserHelper, 
            VariableParser variableParser,
            PathParser pathParser,
            OperatorExpressions operatorExpressions, 
            TerminalExpressions terminalExpressions)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _pathParser = pathParser;

            _actionId = NonTerminalId.VariableStringAssignmentAction;

            // $[VAR] = "[STRING]" #[COMMENT]
            _expressionParser = (terminalExpressions.Variable + operatorExpressions.Assign + terminalExpressions.SymbolWithQuotes).Id(NonTerminalId.VariableStringAssignmentAction);
        }

        public Action Parse(LpNode node)
        {
            var variableName = _variableParser.Parse(node, 0);
            var symbolNode = _parserHelper.FindFirst(node.Children.ElementAt(3), TerminalId.Symbol);
            var text = symbolNode.Match.ToString();
            var result = new VariableStringAssignment(text, variableName);
            return result;
        }
    }
}
