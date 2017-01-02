namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;

    internal class PathParser
    {
        private readonly VariableParser _variableParser;
        private readonly IdentifierParser _identifierParser;
        private readonly IParserHelper _parserHelper;

        public PathParser(
            IParserHelper parserHelper,
            VariableParser variableParser,
            IdentifierParser identifierParser)
        {
            _parserHelper = parserHelper;
            _variableParser = variableParser;
            _identifierParser = identifierParser;
        }

        public Path Parse(LpNode node, int index)
        {
            return Parse(node.Children.ElementAt(index));
        }
        
        public Path Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess(node, "path");

            var pathComponents = new List<PathComponent>();

            var pathComponentNodes = _parserHelper.FindAll(node, NonTerminalId.PathComponent);

            foreach (var pathComponentNode in pathComponentNodes)
            {
                _parserHelper.EnsureSuccess(pathComponentNode, "path component");

                var nodeWithId = _parserHelper.FindFirst(pathComponentNode.Children);
                var pathComponent = ParseComponent(nodeWithId);
                if (pathComponent is IdentifierComponent && pathComponents.Count > 0)
                {
                    throw new ScriptParserException("An identifier can only be used as the root of a path");
                }
                pathComponents.Add(pathComponent);
            }

            return new Path(pathComponents);
        }

        private PathComponent ParseComponent(LpNode node)
        {
            PathComponent result = null;
            string name = null;
            switch(node.Id)
            {
                case TerminalId.Symbol:
                    name = node.Match.ToString();
                    result = new NameComponent(name);
                    break;
                case TerminalId.Variable:
                    var variableName = _variableParser.Parse(node);
                    result = new VariableComponent(variableName);
                    break;
                case TerminalId.Identifier:
                    var identifier = _identifierParser.Parse(node);
                    result = new IdentifierComponent(identifier);
                    break;
            }
            return result;
        }
    }
}
