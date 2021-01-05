namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class NextPathSubjectPartParser : INextPathSubjectPartParser
    {
        public string Id { get; } = nameof(NextPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @">";
        private const string _relationDescription = @"NEXT_OF";

        public NextPathSubjectPartParser(
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
            return new NextPathSubjectPart();
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
