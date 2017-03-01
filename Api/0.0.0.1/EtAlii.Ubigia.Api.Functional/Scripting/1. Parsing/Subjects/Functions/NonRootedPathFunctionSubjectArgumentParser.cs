namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class NonRootedPathFunctionSubjectArgumentParser : INonRootedPathFunctionSubjectArgumentParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "NonRootedPathFunctionSubjectArgument";

        public LpsParser Parser { get { return _parser; } }

        private readonly LpsParser _parser;
        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

        public NonRootedPathFunctionSubjectArgumentParser(
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

            var subject = parts[0] is IsParentOfPathSubjectPart 
                ? (NonRootedPathSubject)new AbsolutePathSubject(parts) 
                : (NonRootedPathSubject)new RelativePathSubject(parts);

            return new NonRootedPathFunctionSubjectArgument(subject);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
            var parts = ((NonRootedPathFunctionSubjectArgument)argument).Subject.Parts;

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
            return argument is NonRootedPathFunctionSubjectArgument;
        }
    }
}