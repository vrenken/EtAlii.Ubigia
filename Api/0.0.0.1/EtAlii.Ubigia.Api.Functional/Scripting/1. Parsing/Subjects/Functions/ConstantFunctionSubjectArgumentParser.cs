namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class ConstantFunctionSubjectArgumentParser : IConstantFunctionSubjectArgumentParser
    {
        public string Id { get; } = "ConstantFunctionSubjectArgument";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IConstantHelper _constantHelper;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

        public ConstantFunctionSubjectArgumentParser(
            INodeValidator nodeValidator,
            IConstantHelper constantHelper,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _constantHelper = constantHelper;
            _nodeFinder = nodeFinder;

            Parser = new LpsParser(Id, true,
                //(Lp.One(c => _constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(TextId)) |
                (Lp.One(c => c == '\"') +
                 Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\"')).OneOrMore().Id(_textId) +
                 Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') +
                 Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c, '\'')).OneOrMore().Id(_textId) +
                 Lp.One(c => c == '\''))
                );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            return new ConstantFunctionSubjectArgument(text);
        }

        public void Validate(FunctionSubjectArgument before, FunctionSubjectArgument argument, int parameterIndex, FunctionSubjectArgument after)
        {
        }

        public bool CanValidate(FunctionSubjectArgument argument)
        {
            return argument is ConstantFunctionSubjectArgument;
        }
    }
}