namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class AddItemActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public AddItemActionParser(
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

            _actionId = NonTerminalId.AddAction;

            // /[PATH]/ += [PATH] #[COMMENT]
            _expressionParser = (pathExpressions.Rooted + terminalExpressions.Separator + operatorExpressions.Add + pathExpressions.NonRooted).Id(NonTerminalId.AddAction);
        }

        public Action Parse(LpNode node)
        {
            var path = _pathParser.Parse(node, 0);
            var itemToAdd = _pathParser.Parse(node, 3);
            var result = new AddItem(path, itemToAdd);
            return result;
        }
    }
}
