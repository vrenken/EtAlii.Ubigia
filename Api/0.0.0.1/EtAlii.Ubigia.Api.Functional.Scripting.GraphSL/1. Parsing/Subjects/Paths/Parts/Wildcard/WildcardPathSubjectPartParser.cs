namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal class WildcardPathSubjectPartParser : IWildcardPathSubjectPartParser
    {
        public string Id { get; } = nameof(WildcardPathSubjectPart);

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
                (Lp.One(c => constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(BeforeTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(BeforeTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(BeforeTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            var afterTextParser = new LpsParser("After", true,
                (Lp.One(c => constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(AfterTextId)) |
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

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is ConstantPathSubjectPart || after is ConstantPathSubjectPart ||
                before is WildcardPathSubjectPart || after is WildcardPathSubjectPart ||
                before is TraversingWildcardPathSubjectPart || after is TraversingWildcardPathSubjectPart)
            {
                throw new ScriptParserException("A wildcard path part cannot be combined with other constant, wildcard or string path parts.");
            }
            else if (partIndex == 0 || (partIndex == 1 && before is ParentPathSubjectPart) && !(before is VariablePathSubjectPart))
            {
                throw new ScriptParserException("A wildcard path part cannot be used at the beginning of a graph path.");
            }
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is WildcardPathSubjectPart;
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
