namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class PathFunctionSubjectArgumentParser : IPathFunctionSubjectArgumentParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "PathFunctionSubjectArgument";

        public LpsParser Parser { get { return _parser; } }

        private readonly LpsParser _parser;
        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

        public PathFunctionSubjectArgumentParser(
            INodeValidator nodeValidator,
            IPathSubjectPartsParser pathSubjectPartsParser)
        {
            _nodeValidator = nodeValidator;
            _pathSubjectPartsParser = pathSubjectPartsParser;
            _parser = new LpsParser(Id, true, _pathSubjectPartsParser.Parser.OneOrMore());
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNodes = node.Children ?? new LpNode[] { };
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();
            return new PathFunctionSubjectArgument(parts);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
            var parts = ((PathFunctionSubjectArgument)argument).Parts;

            for (int i = 0; i < parts.Length; i++)
            {
                var beforePathPart = i > 0 ? parts[i - 1] : null;
                var afterPathPart = i < parts.Length - 1 ? parts[i + 1] : null;
                var part = parts[i];
                _pathSubjectPartsParser.Validate(beforePathPart, part, i, afterPathPart);
            }
        }

        public bool CanValidate(FunctionSubjectArgument argument)
        {
            return argument is PathFunctionSubjectArgument;
        }
    }
}