namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AllUpdatesPathSubjectPartParser : IAllUpdatesPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllUpdatesPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"}}";
        private const string _relationDescription = @"ALL_UPDATES_OF";

        public AllUpdatesPathSubjectPartParser(
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
            return new AllUpdatesPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if(arguments.PartIndex == 0)
            {
                throw new ScriptParserException("The all updates path separator cannot be used to start a path.");
            }
            if (arguments.Before is UpdatesPathSubjectPart || arguments.After is UpdatesPathSubjectPart ||
                arguments.Before is AllUpdatesPathSubjectPart || arguments.After is AllUpdatesPathSubjectPart)
            {
                throw new ScriptParserException("The all updates path separator cannot be combined.");
            }
            if (arguments.After is DowndatePathSubjectPart)
            {
                throw new ScriptParserException("The all updates path separator cannot be followed by a downdate path separator.");
            }
            if (arguments.After is AllDowndatesPathSubjectPart)
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
