namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AllDowndatesPathSubjectPartParser : IAllDowndatesPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllDowndatesPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"{{";
        private const string _relationDescription = @"ALL_DOWNDATES_OF";

        public AllDowndatesPathSubjectPartParser(
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
            return new AllDowndatesPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if(arguments.PartIndex == 0)
            {
                throw new ScriptParserException("The all downdates path separator cannot be used to start a path.");
            }
            if (arguments.Before is DowndatePathSubjectPart || arguments.After is DowndatePathSubjectPart ||
                arguments.Before is AllDowndatesPathSubjectPart || arguments.After is AllDowndatesPathSubjectPart)
            {
                throw new ScriptParserException("The all downdates path separator cannot be combined.");
            }
            if (arguments.After is UpdatesPathSubjectPart)
            {
                throw new ScriptParserException("The all downdates path separator cannot be followed by a update path separator.");
            }
            if (arguments.After is AllUpdatesPathSubjectPart)
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
