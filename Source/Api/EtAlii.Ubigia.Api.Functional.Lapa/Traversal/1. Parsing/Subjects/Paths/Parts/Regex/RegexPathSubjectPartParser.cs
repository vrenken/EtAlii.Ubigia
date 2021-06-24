// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class RegexPathSubjectPartParser : IRegexPathSubjectPartParser
    {
        public string Id => nameof(RegexPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public RegexPathSubjectPartParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var startDoubleQuote = Lp.Term("[\"");//.Debug("StartBracket")
            var endDoubleQuote = Lp.Term("\"]");//.Debug("EndBracket")

            var startSingleQuote = Lp.Term("[\'");//.Debug("StartBracket")
            var endSingleQuote = Lp.Term("\']");//.Debug("EndBracket")

            Parser = new LpsParser
                (Id, true,
                Lp.InBrackets(startDoubleQuote, Lp.OneOrMore(c => c != '\"').Id(TextId, true), endDoubleQuote)
                    |
                Lp.InBrackets(startSingleQuote, Lp.OneOrMore(c => c != '\'').Id(TextId, true), endSingleQuote)
                );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            return new RegexPathSubjectPart(text);
        }
    }
}
