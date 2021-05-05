﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class RegexPathSubjectPartParser : IRegexPathSubjectPartParser
    {
        public string Id { get; } = nameof(RegexPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

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
                Lp.InBrackets(startDoubleQuote, Lp.OneOrMore(c => c != '\"').Id(_textId, true), endDoubleQuote)
                    |
                Lp.InBrackets(startSingleQuote, Lp.OneOrMore(c => c != '\'').Id(_textId, true), endSingleQuote)
                );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            return new RegexPathSubjectPart(text);
        }
    }
}