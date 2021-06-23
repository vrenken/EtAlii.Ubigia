namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class WildcardPathSubjectPartParser : IWildcardPathSubjectPartParser
    {
        public string Id => nameof(WildcardPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string BeforeTextId = "BeforeText";
        private const string AfterTextId = "AfterText";

        public WildcardPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var beforeTextParser = new LpsParser("Before", true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(BeforeTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(BeforeTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(BeforeTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            var afterTextParser = new LpsParser("After", true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(AfterTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(AfterTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(AfterTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            Parser = new LpsParser(Id, true,
                beforeTextParser +
                Lp.One(c => c == '*') +
                afterTextParser
            );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var beforeText = GetMatch(node, BeforeTextId);
            var afterText = GetMatch(node, AfterTextId);
            var pattern = $"{beforeText}*{afterText}";
            return new WildcardPathSubjectPart(pattern);
        }

        private string GetMatch(LpNode node, string id)
        {
            var result = string.Empty;

            var matchingNode = _nodeFinder.FindFirst(node, id);
            if (matchingNode != null)
            {
                result = matchingNode.Match.ToString();
            }
            return result;
        }
    }
}
