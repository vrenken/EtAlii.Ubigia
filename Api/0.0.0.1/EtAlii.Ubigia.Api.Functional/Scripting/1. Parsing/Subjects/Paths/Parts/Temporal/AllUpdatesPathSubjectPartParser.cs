namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AllUpdatesPathSubjectPartParser : IAllUpdatesPathSubjectPartParser
    {
        public string Id { get; } = "AllUpdatesPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"}}";
        private const string RelationDescription = @"ALL_UPDATES_OF";

        public AllUpdatesPathSubjectPartParser(
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
            return new AllUpdatesPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(partIndex == 0)
            {
                throw new ScriptParserException("The all updates path separator cannot be used to start a path.");
            }
            if (before is UpdatesPathSubjectPart || after is UpdatesPathSubjectPart || 
                before is AllUpdatesPathSubjectPart || after is AllUpdatesPathSubjectPart)
            {
                throw new ScriptParserException("The all updates path separator cannot be combined.");
            }
            if (after is DowndatePathSubjectPart)
            {
                throw new ScriptParserException("The all updates path separator cannot be followed by a downdate path separator.");
            }
            if (after is AllDowndatesPathSubjectPart)
            {
                throw new ScriptParserException("The all updates path separator cannot be followed by an all downdates path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllUpdatesPathSubjectPart;
        }
    }
}
