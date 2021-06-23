// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class RootedPathSubjectParser : IRootedPathSubjectParser
    {
        public string Id => nameof(RootedPathSubject);
        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartsParser _pathSubjectPartsParser;

        public RootedPathSubjectParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            IPathSubjectPartsParser pathSubjectPartsParser)
        {
            _nodeValidator = nodeValidator;
            _pathSubjectPartsParser = pathSubjectPartsParser;
            Parser = new LpsParser
                (
                    Id, true,
                    Lp.OneOrMore(c => constantHelper.IsValidConstantCharacter(c)).Id("root") +
                    Lp.Char(':') +
                    //_pathSubjectPartsParser.Parser.OneOrMore().Id("path") +
                    _pathSubjectPartsParser.Parser.ZeroOrMore().Id("path") +
                    Lp.Lookahead(Lp.Not("."))
                );//.Debug("RootedPathSubjectParser", true)
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var root = node.Children.Single(n => n.Id == "root").ToString();
            var childNodes = node.Children.Single(n => n.Id == "path")?.Children ?? Array.Empty<LpNode>();
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

            return new RootedPathSubject(root, parts);
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
