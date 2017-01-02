// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional
{
    using Moppet.Lapa;

    internal class StringConstantSubjectParser : IStringConstantSubjectParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "StringConstantSubject";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public StringConstantSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            IQuotedTextParser quotedTextParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _quotedTextParser = quotedTextParser;

            _parser = new LpsParser(Id, true, Lp.Not('/') + _quotedTextParser.Parser + Lp.Not('/'));
        }

        public ConstantSubject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var quotedTextNode = _nodeFinder.FindFirst(node, _quotedTextParser.Id);
            var text = _quotedTextParser.Parse(quotedTextNode);

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
