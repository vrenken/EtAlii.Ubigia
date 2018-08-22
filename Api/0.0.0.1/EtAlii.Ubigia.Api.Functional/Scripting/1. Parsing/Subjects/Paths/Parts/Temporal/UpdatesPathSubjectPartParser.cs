namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class UpdatesPathSubjectPartParser : IUpdatesPathSubjectPartParser
    {
        public string Id { get; } = "UpdatesPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"}";
        private const string RelationDescription = @"UPDATES_OF";

        public UpdatesPathSubjectPartParser(
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
            return new UpdatesOfPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (partIndex == 0)
            {
                throw new ScriptParserException("The updates path separator cannot be used to start a path.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is UpdatesOfPathSubjectPart;
        }
    }
}
