namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class AssignOperatorParser : IOperatorParser
    {
        public const string Id = "AssignOperator";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;

        public AssignOperatorParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
            _parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Term("<=") + Lp.ZeroOrMore(' '));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public Operator Parse(LpNode node)
        {   
            _parserHelper.EnsureSuccess2(node, Id);
            return new AssignOperator();
        }

        public bool CanValidate(Operator @operator)
        {
            return @operator is AssignOperator;
        }

        public void Validate(SequencePart before, Operator @operator, int partIndex, SequencePart after)
        {
        }
    }
}
