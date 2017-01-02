namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class ItemsOutputActionParser : IActionParser
    {
        private readonly PathParser _pathParser;
        private readonly VariableParser _variableParser;
        private readonly IParserHelper _parserHelper;

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public LpsParser ExpressionParser { get { return _expressionParser; } }
        private readonly LpsParser _expressionParser;

        public ItemsOutputActionParser(
            IParserHelper parserHelper, 
            VariableParser variableParser,
            PathParser pathParser,
            TerminalExpressions terminalExpressions,
            PathExpressions pathExpressions)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _pathParser = pathParser;

            _actionId = NonTerminalId.ItemsOutputAction;

            // /[PATH]/ #[COMMENT]
            _expressionParser = (pathExpressions.Rooted).Id(NonTerminalId.ItemsOutputAction) + terminalExpressions.Separator + Lp.Lookahead(terminalExpressions.Separator.Not());
        }

        public Action Parse(LpNode node)
        {
            var path = _pathParser.Parse(node);
            var result = new ItemsOutput(path);
            return result;
        }
    }
}
