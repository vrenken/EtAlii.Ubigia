// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class RegexPathSubjectPartParser : IRegexPathSubjectPartParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "RegexPathSubjectPart";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";
        private readonly string[] _types;

        public RegexPathSubjectPartParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var startDoubleQuote = Lp.Term("[\"");//.Debug("StartBracket");
            var endDoubleQuote = Lp.Term("\"]");//.Debug("EndBracket");

            var startSingleQuote = Lp.Term("[\'");//.Debug("StartBracket");
            var endSingleQuote = Lp.Term("\']");//.Debug("EndBracket");

            _parser = new LpsParser
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

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            //if (before is ConstantPathSubjectPart || after is ConstantPathSubjectPart)
            //{
            //    throw new ScriptParserException("Two constant path parts cannot be combined.");
            //}
            //if (partIndex != 0 || after == null)
            //{
            //    var constant = (ConstantPathSubjectPart)part;
            //    if (constant.Name == String.Empty)
            //    {
            //        throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
            //    }
            //}
            //if (partIndex == 0 && after != null)
            //{
            //    var constant = (ConstantPathSubjectPart)part;
            //    if (constant.Name == String.Empty)
            //    {
            //        throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
            //    }
            //}
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is RegexPathSubjectPart;
        }
    }
}
