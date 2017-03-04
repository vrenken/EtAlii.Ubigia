namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class TypeValueParser : ITypeValueParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IConstantHelper _constantHelper;

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        public string Id => _id;
        private const string _id = "TypeValue";

        private const string _valueId = "Value";

        public TypeValueParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder,
            IConstantHelper constantHelper)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _constantHelper = constantHelper;

            _parser = new LpsParser(Id, true,
                new LpsParser(_valueId, true,
                    Lp.OneOrMore(c => _constantHelper.IsValidConstantCharacter(c)) +
                    (Lp.Char('.') + Lp.OneOrMore(c => _constantHelper.IsValidConstantCharacter(c))).ZeroOrMore()
                )
            );//.Debug("TypeValueParser", true);
        }


        public string Parse(LpNode node)
        {     
            _nodeValidator.EnsureSuccess(node, Id);
            var type = _nodeFinder.FindFirst(node, _valueId).Match.ToString();
            return type;
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
