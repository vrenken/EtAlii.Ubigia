namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class RemoveOperatorParser : IRemoveOperatorParser
    {
        public string Id { get; } = nameof(RemoveOperator);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        public RemoveOperatorParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("-=") + Lp.ZeroOrMore(' '));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public Operator Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            return new RemoveOperator();
        }

        public bool CanValidate(Operator item)
        {
            return item is RemoveOperator;
        }

        public void Validate(SequencePart before, Operator item, int itemIndex, SequencePart after)
        {
            // Make sure the after SequencePart can actually be removed from the before SequencePart.
        }
    }
}
