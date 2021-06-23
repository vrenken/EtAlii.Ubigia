namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AssignOperatorParser : IAssignOperatorParser
    {
        public string Id => nameof(AssignOperator);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        public AssignOperatorParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("<=") + Lp.ZeroOrMore(' '));

        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public Operator Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AssignOperator();
        }
    }
}
