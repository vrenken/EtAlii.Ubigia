﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal class QuotedTextParser : IQuotedTextParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = "QuotedText";

        private const string TextId = "Text";

        public QuotedTextParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder,
            IConstantHelper constantHelper)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true,
                (Lp.One(c => c == '\"') + Lp.ZeroOrMore(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).Id(TextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.ZeroOrMore(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).Id(TextId) + Lp.One(c => c == '\''))
            );
        }


        public string Parse(LpNode node)
        {     
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            return text;
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
