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

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
