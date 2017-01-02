namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class AddOperatorParser : IAddOperatorParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "AddOperator";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;

        public AddOperatorParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            _parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("+=") + Lp.ZeroOrMore(' '));//.Debug("AddOperatorParser", true);
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
            //var pathToAdd = after as PathSubject;
            //if (pathToAdd != null)
            //{
            //    var firstPath = pathToAdd.Parts.FirstOrDefault();
            //    var startsWithRelation = firstPath is IsParentOfPathSubjectPart;
            //    if (!startsWithRelation)
            //    {
            //        throw new ScriptParserException("The add operation requires a path to start with a relation symbol.");
            //    }
            //}
        }
    }
}
