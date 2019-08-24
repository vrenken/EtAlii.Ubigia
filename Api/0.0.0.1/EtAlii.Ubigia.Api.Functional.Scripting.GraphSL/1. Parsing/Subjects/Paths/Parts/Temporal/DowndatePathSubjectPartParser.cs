namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal class DowndatePathSubjectPartParser : IDowndatePathSubjectPartParser
    {
        public string Id { get; } = nameof(DowndatePathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string RelationId = @"{";
        private const string RelationDescription = @"DOWNDATE_OF";

        public DowndatePathSubjectPartParser(
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
            return new DowndatePathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if(arguments.PartIndex == 0)
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
            return part is DowndatePathSubjectPart;
        }
    }
}
