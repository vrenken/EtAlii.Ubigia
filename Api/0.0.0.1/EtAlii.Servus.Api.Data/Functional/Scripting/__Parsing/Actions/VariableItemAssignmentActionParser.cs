namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class VariableItemAssignmentActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public VariableItemAssignmentActionParser(
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

            _actionId = NonTerminalId.VariableItemAssignmentAction;

            // $[VAR] = /[PATH] #[COMMENT]
            _expressionParser = (terminalExpressions.Variable + operatorExpressions.Assign + pathExpressions.Rooted).Id(NonTerminalId.VariableItemAssignmentAction);
        }

        public Action Parse(LpNode node)
        {
            var variableName = _variableParser.Parse(node, 0);
            var path = _pathParser.Parse(node, 2);
            var result = new VariableItemAssignment(path, variableName);
            return result;
        }
    }
}
