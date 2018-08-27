﻿namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class PreviousPathSubjectPartParser : IPreviousPathSubjectPartParser
    {
        public string Id { get; } = "PreviousPathSubjectPart";

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
//            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId) +
//                 Lp.Lookahead(Lp.Not(".");
//            Parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new PreviousPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(partIndex == 0)
            {
                throw new ScriptParserException("The previous path separator cannot be used to start a path.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is PreviousPathSubjectPart;
        }
    }
}
