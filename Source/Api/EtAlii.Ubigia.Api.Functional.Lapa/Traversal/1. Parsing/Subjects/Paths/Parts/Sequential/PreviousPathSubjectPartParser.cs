// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class PreviousPathSubjectPartParser : IPreviousPathSubjectPartParser
    {
        public string Id => nameof(PreviousPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"<";
        private const string RelationDescription = @"PREVIOUS_OF";

        public PreviousPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            Parser = new LpsParser
            (
                Id, true,
                pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId) +
                Lp.Lookahead(Lp.Not(Lp.ZeroOrMore(' ') + Lp.Char('=')))
            );
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new PreviousPathSubjectPart();
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
