namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class VariableFunctionSubjectArgumentParser : IVariableFunctionSubjectArgumentParser
    {
        public string Id { get; } = nameof(VariableFunctionSubjectArgument);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

        public VariableFunctionSubjectArgumentParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(_textId));
        }

        public FunctionSubjectArgument Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
            return new VariableFunctionSubjectArgument(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
