namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AllDowndatesPathSubjectPartParser : IAllDowndatesPathSubjectPartParser
    {
        public string Id { get; } = "AllDowndatesPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"{{";
        private const string RelationDescription = @"ALL_DOWNDATES_OF";

        public AllDowndatesPathSubjectPartParser(
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
            return new AllDowndatesPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(partIndex == 0)
            {
                throw new ScriptParserException("The all downdates path separator cannot be used to start a path.");
            }
            if (before is DowndatePathSubjectPart || after is DowndatePathSubjectPart || 
                before is AllDowndatesPathSubjectPart || after is AllDowndatesPathSubjectPart)
            {
                throw new ScriptParserException("The all downdates path separator cannot be combined.");
            }
            if (after is UpdatesPathSubjectPart)
            {
                throw new ScriptParserException("The all downdates path separator cannot be followed by a update path separator.");
            }
            if (after is AllUpdatesPathSubjectPart)
            {
                throw new ScriptParserException("The all downdates path separator cannot be followed by an all update path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllDowndatesPathSubjectPart;
        }
    }
}
