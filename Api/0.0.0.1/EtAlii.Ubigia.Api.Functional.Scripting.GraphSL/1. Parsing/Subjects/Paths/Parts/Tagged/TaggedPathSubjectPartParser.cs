namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal class TaggedPathSubjectPartParser : ITaggedPathSubjectPartParser
    {
        public string Id { get; } = nameof(TaggedPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string NameTextId = "NameText";
        private const string TagTextId = "TagText";

        public TaggedPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var nameTextParser = new LpsParser("Name", true,
                (Lp.One(c => constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(NameTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(NameTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(NameTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            var tagTextParser = new LpsParser("Tag", true,
                (Lp.One(c => constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(TagTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(TagTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(TagTextId) + Lp.One(c => c == '\''))
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
            var nameText = GetMatch(node, NameTextId);
            var tagText = GetMatch(node, TagTextId);
            return new TaggedPathSubjectPart(nameText, tagText);
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is ConstantPathSubjectPart || after is ConstantPathSubjectPart ||
                before is WildcardPathSubjectPart || after is WildcardPathSubjectPart ||
                before is TaggedPathSubjectPart || after is TaggedPathSubjectPart ||
                before is TraversingWildcardPathSubjectPart || after is TraversingWildcardPathSubjectPart)
            {
                throw new ScriptParserException("A tagged path part cannot be combined with other constant, tagged, wildcard or string path parts.");
            }
            else if (partIndex == 0 || (partIndex == 1 && before is ParentPathSubjectPart) && (before is VariablePathSubjectPart) == false)
            {
                throw new ScriptParserException("A tagged path part cannot be used at the beginning of a graph path.");
            }
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is TaggedPathSubjectPart;
        }

        private string GetMatch(LpNode node, string id)
        {
            var result = String.Empty;

            var matchingNode = _nodeFinder.FindFirst(node, id);
            if (matchingNode != null)
            {
                result = matchingNode.Match.ToString();
            }
            return result;
        }
    }
}
