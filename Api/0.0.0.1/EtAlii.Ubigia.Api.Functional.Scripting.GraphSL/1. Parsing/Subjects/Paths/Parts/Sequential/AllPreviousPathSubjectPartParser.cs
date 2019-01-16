namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AllPreviousPathSubjectPartParser : IAllPreviousPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllPreviousPathSubjectPartParser);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"<<";
        private const string RelationDescription = @"ALL_PREVIOUS_OF";

        public AllPreviousPathSubjectPartParser(
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
            
//            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
//            Parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AllPreviousPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(partIndex == 0)
            {
                throw new ScriptParserException("The all previous path separator cannot be used to start a path.");
            }
            if (before is PreviousPathSubjectPart || after is PreviousPathSubjectPart || 
                before is AllPreviousPathSubjectPart || after is AllPreviousPathSubjectPart)
            {
                throw new ScriptParserException("The all previous path separator cannot be combined.");
            }
            if (after is NextPathSubjectPart)
            {
                throw new ScriptParserException("The all previous path separator cannot be followed by a next path separator.");
            }
            if (after is AllNextPathSubjectPart)
            {
                throw new ScriptParserException("The all previous path separator cannot be followed by an all next path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllPreviousPathSubjectPart;
        }
    }
}
