namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal class WildcardPathSubjectPartParser : IWildcardPathSubjectPartParser
    {
        public string Id => _id;
        private readonly string _id = "WildcardPathSubjectPart";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IConstantHelper _constantHelper;
        private readonly INodeFinder _nodeFinder;
        private const string _beforeTextId = "BeforeText";
        private const string _afterTextId = "AfterText";

        public WildcardPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _constantHelper = constantHelper;
            _nodeFinder = nodeFinder;

            var beforeTextParser = new LpsParser("Before", true,
                (Lp.One(c => _constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(_beforeTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_beforeTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_beforeTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            var afterTextParser = new LpsParser("After", true,
                (Lp.One(c => _constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(_afterTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_afterTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_afterTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            _parser = new LpsParser(Id, true,
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
            var beforeText = GetMatch(node, _beforeTextId);
            var afterText = GetMatch(node, _afterTextId);
            var pattern = String.Format("{0}*{1}", beforeText, afterText);
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
            else if (partIndex == 0 || (partIndex == 1 && before is IsParentOfPathSubjectPart) && (before is VariablePathSubjectPart) == false)
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
