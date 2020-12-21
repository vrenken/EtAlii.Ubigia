namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AllPreviousPathSubjectPartParser : IAllPreviousPathSubjectPartParser
    {
        public string Id { get; } = nameof(AllPreviousPathSubjectPartParser);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"<<";
        private const string _relationDescription = @"ALL_PREVIOUS_OF";

        public AllPreviousPathSubjectPartParser(
            INodeValidator nodeValidator,
            IPathRelationParserBuilder pathRelationParserBuilder)
        {
            _nodeValidator = nodeValidator;

            Parser = new LpsParser
            (
                Id, true,
                pathRelationParserBuilder.CreatePathRelationParser(_relationDescription, _relationId) +
                Lp.Lookahead(Lp.Not(Lp.ZeroOrMore(' ') + Lp.Char('=')))
            );

//            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId)
//            Parser = new LpsParser(Id, true, relationParser)
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AllPreviousPathSubjectPart();
        }


        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if(arguments.PartIndex == 0)
            {
                throw new ScriptParserException("The all previous path separator cannot be used to start a path.");
            }
            if (arguments.Before is PreviousPathSubjectPart || arguments.After is PreviousPathSubjectPart ||
                arguments.Before is AllPreviousPathSubjectPart || arguments.After is AllPreviousPathSubjectPart)
            {
                throw new ScriptParserException("The all previous path separator cannot be combined.");
            }
            if (arguments.After is NextPathSubjectPart)
            {
                throw new ScriptParserException("The all previous path separator cannot be followed by a next path separator.");
            }
            if (arguments.After is AllNextPathSubjectPart)
            {
                throw new ScriptParserException("The all previous path separator cannot be followed by an all next path separator.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is AllPreviousPathSubjectPart;
        }
    }
}
