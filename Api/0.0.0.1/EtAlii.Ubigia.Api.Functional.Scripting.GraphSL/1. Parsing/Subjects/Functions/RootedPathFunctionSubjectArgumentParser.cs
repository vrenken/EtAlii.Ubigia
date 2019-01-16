﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class RootedPathFunctionSubjectArgumentParser : IRootedPathFunctionSubjectArgumentParser
    {
        public string Id { get; } = nameof(RootedPathFunctionSubjectArgument);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

        public RootedPathFunctionSubjectArgumentParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            IPathSubjectPartsParser pathSubjectPartsParser)
        {
            _nodeValidator = nodeValidator;
            _pathSubjectPartsParser = pathSubjectPartsParser;
            Parser = new LpsParser(Id, true,
                    Lp.OneOrMore(c => constantHelper.IsValidConstantCharacter(c)).Id("root") +
                    Lp.Char(':') +
                    _pathSubjectPartsParser.Parser.ZeroOrMore().Id("path")
                );//.Debug("RootedPathFunctionSubjectArgumentParser", true); ;
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var root = node.Children.Single(n => n.Id == "root").ToString();
            var childNodes = node.Children.Single(n => n.Id == "path")?.Children ?? new LpNode[] { };
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

            var subject = new RootedPathSubject(root, parts);

            return new RootedPathFunctionSubjectArgument(subject);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
            var parts = ((RootedPathFunctionSubjectArgument)argument).Subject.Parts;

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
            return argument is RootedPathFunctionSubjectArgument;
        }
    }
}