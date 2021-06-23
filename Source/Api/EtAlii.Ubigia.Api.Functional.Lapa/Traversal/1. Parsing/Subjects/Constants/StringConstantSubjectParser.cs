// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class StringConstantSubjectParser : IStringConstantSubjectParser
    {
        public string Id { get; } = nameof(StringConstantSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

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
                    Id, true,
                    //Lp.Char('/').Not().Debug("Start", true) +
                    (Lp.OneOrMore(c => constantHelper.IsValidConstantCharacter(c)).Wrap(_textId) |
                    //new LpsParser("Start", false, Lp.Char('/')).Not().Debug("Bracket-Start", true) +
                    _quotedTextParser.Parser) + //.Debug("Content", true)) + //.Look(c => c != '/', c => c != '/').Debug("Look", true)
                    //new LpsParser(Lp.Char('/').Not().Debug("Stop", true) | Lp.End)
                    Lp.End
                );//.Debug("StringConstantSubjectParser", true)
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
                text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            }
            return new StringConstantSubject(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
