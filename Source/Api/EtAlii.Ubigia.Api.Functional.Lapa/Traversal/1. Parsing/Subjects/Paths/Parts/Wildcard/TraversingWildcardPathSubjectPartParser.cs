namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class TraversingWildcardPathSubjectPartParser : ITraversingWildcardPathSubjectPartParser
    {
        public string Id => nameof(TraversingWildcardPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IIntegerValueParser _integerValueParser;
        private const string LimitTextId = "LimitText";

        public TraversingWildcardPathSubjectPartParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            IIntegerValueParser integerValueParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _integerValueParser = integerValueParser;

            Parser = new LpsParser(Id, true,
                Lp.One(c => c == '*') +

                new LpsParser(LimitTextId, true, _integerValueParser.Parser).Maybe() +
                Lp.One(c => c == '*'));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var integerNode = _nodeFinder.FindFirst(node, _integerValueParser.Id);
            var limit = integerNode != null ? _integerValueParser.Parse(integerNode) : 0;
            return new TraversingWildcardPathSubjectPart(limit);
        }
    }
}
