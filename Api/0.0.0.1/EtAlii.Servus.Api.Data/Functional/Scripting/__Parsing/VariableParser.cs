namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Linq;

    internal class VariableParser
    {
        private readonly IParserHelper _parserHelper;

        public VariableParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
        }

        public string Parse(LpNode node, int index)
        {
            return Parse(node.Children.ElementAt(index));
        }

        public string Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess(node, "variable");

            var variableNode = _parserHelper.FindFirst(node, TerminalId.Variable);
            return variableNode.Match.ToString();
        }

    }
}
