namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AllNextPathSubjectPartParser : IAllNextPathSubjectPartParser
    {
        public string Id { get; } = "AllNextPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @">>";
        private const string RelationDescription = @"ALL_NEXT_OF";

        public AllNextPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            Parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AllNextPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(partIndex == 0)
            {
                throw new ScriptParserException("The all next path separator cannot be used to start a path.");
            }
            if (before is NextPathSubjectPart || after is NextPathSubjectPart || 
                before is AllNextPathSubjectPart || after is AllNextPathSubjectPart)
            {
                throw new ScriptParserException("The all next path separator cannot be combined.");
            }
            if (after is PreviousPathSubjectPart)
            {
                throw new ScriptParserException("The all next path separator cannot be followed by a previous path separator.");
            }
            if (after is AllPreviousPathSubjectPart)
            {
                throw new ScriptParserException("The all next path separator cannot be followed by an all previous path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllNextPathSubjectPart;
        }
    }
}
