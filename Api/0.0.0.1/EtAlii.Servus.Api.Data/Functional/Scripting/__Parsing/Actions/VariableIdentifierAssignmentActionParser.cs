namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class VariableIdentifierAssignmentActionParser : IActionParser
    {
        private readonly IdentifierParser _identifierParser;
        private readonly VariableParser _variableParser;
        private readonly ParserHelper _parserHelper;

        public VariableIdentifierAssignmentActionParser(
            ParserHelper parserHelper, 
            VariableParser variableParser,
            IdentifierParser identifierParser)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _identifierParser = identifierParser;

            _actionId = NonTerminalId.VariableIdentifierAssignmentAction;
        }

        public string ActionId { get { return _actionId; } }
        private readonly string _actionId;

        public Action Parse(LpNode node)
        {
            var variableName = _variableParser.Parse(node.Children.First());
            var identifier = _identifierParser.Parse(node.Children.Skip(3).First());
            var result = new VariableIdentifierAssignment(identifier, variableName);
            return result;
        }
    }
}
