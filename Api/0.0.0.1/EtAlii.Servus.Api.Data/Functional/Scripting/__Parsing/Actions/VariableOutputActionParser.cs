namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class VariableOutputActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public VariableOutputActionParser(
            IParserHelper parserHelper, 
            VariableParser variableParser, 
            PathParser pathParser,
            TerminalExpressions terminalExpressions)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _pathParser = pathParser;

            _actionId = NonTerminalId.VariableOutputAction;

            // $[VAR] #[COMMENT]
            _expressionParser = (terminalExpressions.Variable).Id(NonTerminalId.VariableOutputAction);
        }

        public Action Parse(LpNode node)
        {
            var variableName = _variableParser.Parse(node);
            var result = new VariableOutput(variableName);
            return result;
        }
    }
}
