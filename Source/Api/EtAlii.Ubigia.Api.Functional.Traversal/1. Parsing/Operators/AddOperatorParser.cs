namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class AddOperatorParser : IAddOperatorParser
    {
        public string Id { get; } = nameof(AddOperator);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        public AddOperatorParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("+=") + Lp.ZeroOrMore(' '));//.Debug("AddOperatorParser", true)
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

        public bool CanValidate(Operator item)
        {
            return item is AddOperator;
        }

        public void Validate(SequencePart before, Operator item, int itemIndex, SequencePart after)
        {
            //var pathToAdd = after as PathSubject
            //if [pathToAdd ! = null]
            //[
            //    var firstPath = pathToAdd.Parts.FirstOrDefault()
            //    var startsWithRelation = firstPath is ParentPathSubjectPart
            //    if [!startsWithRelation]
            //    [
            //        throw new ScriptParserException("The add operation requires a path to start with a relation symbol.")
            //    ]
            //]
        }
    }
}
