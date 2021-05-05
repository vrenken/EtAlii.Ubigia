namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class PreviousPathSubjectPartParser : IPreviousPathSubjectPartParser
    {
        public string Id { get; } = nameof(PreviousPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"<";
        private const string _relationDescription = @"PREVIOUS_OF";

        public PreviousPathSubjectPartParser(
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
//            var relationParser = pathRelationParserBuilder.CreatePathRelationParser(RelationDescription, RelationId) +
//                 Lp.Lookahead(Lp.Not(".")
//            Parser = new LpsParser(Id, true, relationParser)
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new PreviousPathSubjectPart();
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
