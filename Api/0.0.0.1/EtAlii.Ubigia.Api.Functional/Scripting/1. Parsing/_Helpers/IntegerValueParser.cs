namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal class IntegerValueParser : IIntegerValueParser
    {
        private readonly INodeValidator _nodeValidator;

        public LpsParser Parser { get; }

        public string Id => _id;
        private const string _id = "IntegerValue";

        public IntegerValueParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.One(c => c == '-' || c == '+').Maybe() + Lp.OneOrMore(c => Char.IsDigit(c)));
        }


        public int Parse(LpNode node)
        {     
            _nodeValidator.EnsureSuccess(node, Id);
            var text = node.Match.ToString();
            return Int32.Parse(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
