// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class StringConstantSubjectParser : IStringConstantSubjectParser
    {
        public string Id { get; } = nameof(StringConstantSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public StringConstantSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            IQuotedTextParser quotedTextParser,
            IConstantHelper constantHelper)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _quotedTextParser = quotedTextParser;

            Parser = new LpsParser
                (
                    Id, true, (Lp.OneOrMore(c => constantHelper.IsValidConstantCharacter(c)).Wrap(TextId) | _quotedTextParser.Parser) + //.Debug("Content", true)) + //.Look(c => c != '/', c => c != '/').Debug("Look", true)
                    Lp.End
                );
        }

        public ConstantSubject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            string text;
            var quotedTextNode = _nodeFinder.FindFirst(node, _quotedTextParser.Id);
            if (quotedTextNode != null)
            {
                text = _quotedTextParser.Parse(quotedTextNode);
            }
            else
            {
                text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            }
            return new StringConstantSubject(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after)
        {
        }

        public bool CanValidate(ConstantSubject constantSubject)
        {
            return constantSubject is StringConstantSubject;
        }
    }
}
