namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AllNextPathSubjectPartParser : IAllNextPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllNextPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @">>";
        private const string _relationDescription = @"ALL_NEXT_OF";

        public AllNextPathSubjectPartParser(
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
            return new AllNextPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if(arguments.PartIndex == 0)
            {
                throw new ScriptParserException("The all next path separator cannot be used to start a path.");
            }
            if (arguments.Before is NextPathSubjectPart || arguments.After is NextPathSubjectPart ||
                arguments.Before is AllNextPathSubjectPart || arguments.After is AllNextPathSubjectPart)
            {
                throw new ScriptParserException("The all next path separator cannot be combined.");
            }
            if (arguments.After is PreviousPathSubjectPart)
            {
                throw new ScriptParserException("The all next path separator cannot be followed by a previous path separator.");
            }
            if (arguments.After is AllPreviousPathSubjectPart)
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
