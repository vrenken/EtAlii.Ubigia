namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class ItemOutputActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public ItemOutputActionParser(
            IParserHelper parserHelper, 
            VariableParser variableParser,
            PathParser pathParser,
            TerminalExpressions terminalExpressions,
            PathExpressions pathExpressions)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _pathParser = pathParser;

            _actionId = NonTerminalId.ItemOutputAction;

            // /[PATH] #[COMMENT]
            _expressionParser = (pathExpressions.Rooted).Id(NonTerminalId.ItemOutputAction);
        }

        public Action Parse(LpNode node)
        {
            var path = _pathParser.Parse(node);
            var result = new ItemOutput(path);
            return result;
        }
    }
}
