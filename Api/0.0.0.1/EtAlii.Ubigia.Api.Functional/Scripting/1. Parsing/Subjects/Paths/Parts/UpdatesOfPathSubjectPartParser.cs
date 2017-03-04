namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class UpdatesOfPathSubjectPartParser : IUpdatesOfPathSubjectPartParser
    {
        public string Id => _id;
        private readonly string _id = "UpdatesOfPathSubjectPart";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"}";
        private const string RelationDescription = @"UPDATES_OF";

        public UpdatesOfPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId);
            _parser = new LpsParser(Id, true, relationParser);
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
