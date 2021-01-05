namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class DowndatePathSubjectPartParser : IDowndatePathSubjectPartParser
    {
        public string Id { get; } = nameof(DowndatePathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        private const string _relationId = @"{";
        private const string _relationDescription = @"DOWNDATE_OF";

        public DowndatePathSubjectPartParser(
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
            return new DowndatePathSubjectPart();
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
