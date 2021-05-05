namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class TaggedPathSubjectPartParser : ITaggedPathSubjectPartParser
    {
        public string Id { get; } = nameof(TaggedPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _nameTextId = "NameText";
        private const string _tagTextId = "TagText";

        public TaggedPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var nameTextParser = new LpsParser("Name", true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(_nameTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_nameTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_nameTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            var tagTextParser = new LpsParser("Tag", true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(_tagTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_tagTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_tagTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            Parser = new LpsParser(Id, true,
                nameTextParser +
                Lp.One(c => c == '#') +
                tagTextParser
            );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var nameText = GetMatch(node, _nameTextId);
            var tagText = GetMatch(node, _tagTextId);
            return new TaggedPathSubjectPart(nameText, tagText);
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
