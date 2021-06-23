namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class RootedPathFunctionSubjectArgumentParser : IRootedPathFunctionSubjectArgumentParser
    {
        public string Id => nameof(RootedPathFunctionSubjectArgument);

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
                );//.Debug("RootedPathFunctionSubjectArgumentParser", true)
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var root = node.Children.Single(n => n.Id == "root").ToString();
            var childNodes = node.Children.Single(n => n.Id == "path")?.Children ?? Array.Empty<LpNode>();
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

            var subject = new RootedPathSubject(root, parts);

            return new RootedPathFunctionSubjectArgument(subject);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
