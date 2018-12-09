// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal class ConstantPathSubjectPartParser : IConstantPathSubjectPartParser
    {
        public string Id { get; } = "ConstantPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public ConstantPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            Parser = new LpsParser(Id, true,
                (Lp.One(c => constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(TextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).ZeroOrMore().Id(TextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).ZeroOrMore().Id(TextId) + Lp.One(c => c == '\'')) 
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
            return new ConstantPathSubjectPart(text);
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is ConstantPathSubjectPart || after is ConstantPathSubjectPart)
            {
                throw new ScriptParserException("Two constant path parts cannot be combined.");
            }
            if (partIndex != 0 || after == null)
            {
                var constant = (ConstantPathSubjectPart)part;
                if (constant.Name == String.Empty)
                {
                    throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
                }
            }
            if (partIndex == 0 && after != null)
            {
                var constant = (ConstantPathSubjectPart)part;
                if (constant.Name == String.Empty)
                {
                    throw new ScriptParserException("An empty constant path part is only allowed in single part paths.");
                }
            }
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is ConstantPathSubjectPart;
        }
    }
}
