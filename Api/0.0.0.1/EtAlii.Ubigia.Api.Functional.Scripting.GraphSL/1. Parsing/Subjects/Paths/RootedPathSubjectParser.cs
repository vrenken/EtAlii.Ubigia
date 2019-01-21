// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class RootedPathSubjectParser : IRootedPathSubjectParser
    {
        private const string Id = nameof(RootedPathSubject);
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
                    _pathSubjectPartsParser.Parser.ZeroOrMore().Id("path") +
                    Lp.Lookahead(Lp.Not("."))
                );
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var root = node.Children.Single(n => n.Id == "root").ToString();
            var childNodes = node.Children.Single(n => n.Id == "path")?.Children ?? new LpNode[] { };
            var parts = childNodes.Select(childNode => _pathSubjectPartsParser.Parse(childNode)).ToArray();

            return new RootedPathSubject(root, parts);
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            var rootedPathSubject = subject as RootedPathSubject;
            var parts = rootedPathSubject.Parts;

            for (int i = 0; i < parts.Length; i++)
            {
                var beforePathPart = i > 0 ? parts[i - 1] : null;
                var afterPathPart = i < parts.Length - 1 ? parts[i + 1] : null;
                var part = parts[i];
                _pathSubjectPartsParser.Validate(beforePathPart, part, i, afterPathPart);
            }
        }

        public bool CanValidate(Subject subject)
        {
            return subject is RootedPathSubject;
        }
    }
}
