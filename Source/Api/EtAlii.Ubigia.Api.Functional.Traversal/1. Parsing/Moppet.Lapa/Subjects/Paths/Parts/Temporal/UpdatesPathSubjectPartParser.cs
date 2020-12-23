namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class UpdatesPathSubjectPartParser : IUpdatesPathSubjectPartParser
    {
        public string Id { get; } = nameof(UpdatesPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"}";
        private const string _relationDescription = @"UPDATES_OF";

        public UpdatesPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(_relationDescription, _relationId);
            Parser = new LpsParser(Id, true, relationParser);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new UpdatesPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.PartIndex == 0)
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
            return part is UpdatesPathSubjectPart;
        }
    }
}
