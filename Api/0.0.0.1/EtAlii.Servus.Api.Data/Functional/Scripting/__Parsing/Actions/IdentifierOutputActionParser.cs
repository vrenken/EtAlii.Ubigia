namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class IdentifierOutputActionParser : IActionParser
    {
        private readonly VariableParser _variableParser;
        private readonly IdentifierParser _identifierParser;
        private readonly ParserHelper _parserHelper;

        public IdentifierOutputActionParser(ParserHelper parserHelper, VariableParser variableParser, IdentifierParser identifierParser)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _identifierParser = identifierParser;

            _actionId = NonTerminalId.IdentifierOutputAction;
        }

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public Action Parse(LpNode node)
        {
            var identifier = _identifierParser.Parse(node);
            var result = new IdentifierOutput(identifier);
            return result;
        }
    }
}
