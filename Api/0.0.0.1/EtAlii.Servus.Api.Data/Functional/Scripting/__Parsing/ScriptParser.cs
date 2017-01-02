namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class ScriptParser : IScriptParser
    {
        private readonly ActionsParser _actionsParser;
        private readonly IParserHelper _parserHelper;
        private readonly LpsParser _sequence;

        /// The main constructor.
        public ScriptParser(
            IParserHelper parserHelper,
            ActionsParser actionsParser)
        {
            _parserHelper = parserHelper;
            _actionsParser = actionsParser;

            _sequence = new LpsParser(NonTerminalId.Sequence, wrapNode: true, recurse: true)
            {
                Parser = _actionsParser.ExpressionParser
            };

        }

        public Script Parse(string text)
        {
            var node = _sequence.Do(text);

            _parserHelper.EnsureSuccess(node, "script", true, text);

            var script = ParseScript(node.Children);
            return script;
        }

        private Script ParseScript(IEnumerable<LpNode> children)
        {
            var actions = _actionsParser.Parse(children);
            return new Script(actions);
        }
    }
}
