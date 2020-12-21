namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class NonRootedPathFunctionSubjectArgumentParser : INonRootedPathFunctionSubjectArgumentParser
    {
        public string Id { get; } = nameof(NonRootedPathFunctionSubjectArgument);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

        public NonRootedPathFunctionSubjectArgumentParser(
            INodeValidator nodeValidator,
            IPathSubjectPartsParser pathSubjectPartsParser)
        {
            _nodeValidator = nodeValidator;
            _pathSubjectPartsParser = pathSubjectPartsParser;
            Parser = new LpsParser(Id, true, _pathSubjectPartsParser.Parser.OneOrMore());
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNodes = node.Children ?? Array.Empty<LpNode>();
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

            var subject = parts[0] is ParentPathSubjectPart
                ? new AbsolutePathSubject(parts)
                : (NonRootedPathSubject)new RelativePathSubject(parts);

            return new NonRootedPathFunctionSubjectArgument(subject);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
            var subject = ((NonRootedPathFunctionSubjectArgument) argument).Subject;
            var parts = subject.Parts;

            for (var i = 0; i < parts.Length; i++)
            {
                var beforePathPart = i > 0 ? parts[i - 1] : null;
                var afterPathPart = i < parts.Length - 1 ? parts[i + 1] : null;
                var part = parts[i];
                var arguments = new PathSubjectPartParserArguments(subject, beforePathPart, part, i, afterPathPart);
                _pathSubjectPartsParser.Validate(arguments);
            }
        }

        public bool CanValidate(FunctionSubjectArgument argument)
        {
            return argument is NonRootedPathFunctionSubjectArgument;
        }
    }
}
