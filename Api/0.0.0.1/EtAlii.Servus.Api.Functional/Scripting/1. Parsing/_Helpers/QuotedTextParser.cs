namespace EtAlii.Servus.Api.Functional
{
    using Moppet.Lapa;

    internal class QuotedTextParser : IQuotedTextParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IConstantHelper _constantHelper;

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        public string Id { get { return _id; } }
        private const string _id = "QuotedText";

        private const string _textId = "Text";

        public QuotedTextParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder,
            IConstantHelper constantHelper)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _constantHelper = constantHelper;
            _parser = new LpsParser(Id, true,
                (Lp.One(c => c == '\"') + Lp.ZeroOrMore(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\"')).Id(_textId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.ZeroOrMore(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\'')).Id(_textId) + Lp.One(c => c == '\''))
            );
        }


        public string Parse(LpNode node)
        {     
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            return text;
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
