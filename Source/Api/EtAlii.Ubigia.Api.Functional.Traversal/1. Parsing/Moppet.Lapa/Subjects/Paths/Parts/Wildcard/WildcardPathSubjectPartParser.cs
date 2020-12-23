namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class WildcardPathSubjectPartParser : IWildcardPathSubjectPartParser
    {
        public string Id { get; } = nameof(WildcardPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _beforeTextId = "BeforeText";
        private const string _afterTextId = "AfterText";

        public WildcardPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var beforeTextParser = new LpsParser("Before", true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(_beforeTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_beforeTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_beforeTextId) + Lp.One(c => c == '\''))
            ).Maybe();

            var afterTextParser = new LpsParser("After", true,
                (Lp.One(constantHelper.IsValidConstantCharacter).OneOrMore().Id(_afterTextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_afterTextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_afterTextId) + Lp.One(c => c == '\''))
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
            var beforeText = GetMatch(node, _beforeTextId);
            var afterText = GetMatch(node, _afterTextId);
            var pattern = $"{beforeText}*{afterText}";
            return new WildcardPathSubjectPart(pattern);
        }

        public void Validate(PathSubjectPartParserArguments arguments)
        {
            if (arguments.Before is ConstantPathSubjectPart || arguments.After is ConstantPathSubjectPart ||
                arguments.Before is WildcardPathSubjectPart || arguments.After is WildcardPathSubjectPart ||
                arguments.Before is TaggedPathSubjectPart || arguments.After is TaggedPathSubjectPart ||
                arguments.Before is TraversingWildcardPathSubjectPart || arguments.After is TraversingWildcardPathSubjectPart)
            {
                throw new ScriptParserException("A wildcard path part cannot be combined with other constant, tagged, wildcard or string path parts.");
            }
            else if ((arguments.PartIndex == 0 && arguments.Subject is NonRootedPathSubject) ||
                     (arguments.PartIndex == 1 && arguments.Before is ParentPathSubjectPart) && !(arguments.Before is VariablePathSubjectPart))
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
