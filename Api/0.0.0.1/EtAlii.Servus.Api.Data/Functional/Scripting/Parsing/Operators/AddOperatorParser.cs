namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;

    internal class AddOperatorParser : IOperatorParser
    {
        public const string Id = "AddOperator";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;

        public AddOperatorParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
            _parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("+=") + Lp.ZeroOrMore(' '));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public Operator Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            return new AddOperator();
        }

        public bool CanValidate(Operator @operator)
        {
            return @operator is AddOperator;
        }

        public void Validate(SequencePart before, Operator @operator, int partIndex, SequencePart after)
        {
            var pathToAdd = after as PathSubject;
            if (pathToAdd != null)
            {
                var firstPath = pathToAdd.Parts.FirstOrDefault();
                var startsWithRelation = firstPath is IsParentOfPathSubjectPart;
                if (!startsWithRelation)
                {
                    throw new ScriptParserException("The add operation requires a path to start with a relation symbol.");
                }
            }
        }
    }
}
