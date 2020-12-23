namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class AssignOperatorParser : IAssignOperatorParser
    {
        public string Id { get; } = nameof(AssignOperator);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly ISelector<SequencePart, SequencePart, Action> _validationSelector;

        public AssignOperatorParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("<=") + Lp.ZeroOrMore(' '));

            _validationSelector = new Selector2<SequencePart, SequencePart, Action>()
                .Register((b, a) => b is PathSubject && a is PathSubject, () => { throw new ScriptParserException("The assign operator cannot assign a path to another path."); });
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

        public bool CanValidate(Operator item)
        {
            return item is AssignOperator;
        }

        public void Validate(SequencePart before, Operator item, int itemIndex, SequencePart after)
        {
            var validation = _validationSelector.TrySelect(before, after);
            validation?.Invoke();
        }
    }
}
