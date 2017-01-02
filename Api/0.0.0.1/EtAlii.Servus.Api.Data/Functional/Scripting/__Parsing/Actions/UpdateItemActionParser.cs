namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class UpdateItemActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public UpdateItemActionParser(
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

            _actionId = NonTerminalId.UpdateAction;

            // /[PATH] <= $[VAR] #[COMMENT]
            _expressionParser = (pathExpressions.Rooted + operatorExpressions.Assign + terminalExpressions.Variable).Id(NonTerminalId.UpdateAction);
        }

        public Action Parse(LpNode node)
        {
            var path = _pathParser.Parse(node, 0);
            var variableName = _variableParser.Parse(node, 2);
            var result = new UpdateItem(path, variableName);
            return result;
        }
    }
}
