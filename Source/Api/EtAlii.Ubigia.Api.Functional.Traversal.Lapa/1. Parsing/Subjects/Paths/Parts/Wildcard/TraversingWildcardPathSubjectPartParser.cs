namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class TraversingWildcardPathSubjectPartParser : ITraversingWildcardPathSubjectPartParser
    {
        public string Id { get; } = nameof(TraversingWildcardPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IIntegerValueParser _integerValueParser;
        private const string _limitTextId = "LimitText";

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

                new LpsParser(_limitTextId, true, _integerValueParser.Parser).Maybe() +
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

        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ConstantPathSubjectPart || arguments.After is ConstantPathSubjectPart ||
                arguments.Before is WildcardPathSubjectPart || arguments.After is WildcardPathSubjectPart ||
                arguments.Before is TaggedPathSubjectPart || arguments.After is TaggedPathSubjectPart ||
                arguments.Before is TraversingWildcardPathSubjectPart || arguments.After is TraversingWildcardPathSubjectPart)
            {
                throw new ScriptParserException("A traversing wildcard path part cannot be combined with other constant, tagged, wildcard or string path parts.");
            }
            //else if [partIndex = = 0 | | partIndex = = 1 & & [before is VariablePathSubjectPart] = = false]
            //[
            //    throw new ScriptParserException["A traversing wildcard path part cannot be used at the beginning of a graph path."]
            //    Not true with rooted paths.
            //]
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is TraversingWildcardPathSubjectPart;
        }
    }
}
