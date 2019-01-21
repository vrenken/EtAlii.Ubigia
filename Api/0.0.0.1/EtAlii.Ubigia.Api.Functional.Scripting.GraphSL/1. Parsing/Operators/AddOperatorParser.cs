namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AddOperatorParser : IAddOperatorParser
    {
        public string Id { get; } = "AddOperator";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        public AddOperatorParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("+=") + Lp.ZeroOrMore(' '));//.Debug("AddOperatorParser", true);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public Operator Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new AddOperator();
        }

        public bool CanValidate(Operator @operator)
        {
            return @operator is AddOperator;
        }

        public void Validate(SequencePart before, Operator @operator, int partIndex, SequencePart after)
        {
            // Validate the add operation.
        }
    }
}
