namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class DowndateOfPathSubjectPartParser : IDowndateOfPathSubjectPartParser
    {
        public string Id { get; } = "DowndateOfPathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"{";
        private const string RelationDescription = @"DOWNDATE_OF";

        public DowndateOfPathSubjectPartParser(
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
            return new DowndateOfPathSubjectPart();
        }


        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if(partIndex == 0)
            {
                throw new ScriptParserException("The downdate path separator cannot be used to start a path.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is DowndateOfPathSubjectPart;
        }
    }
}
