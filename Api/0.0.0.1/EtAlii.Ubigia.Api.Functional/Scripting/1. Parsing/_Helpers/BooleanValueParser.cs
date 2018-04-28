namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using Moppet.Lapa;

    internal class BooleanValueParser : IBooleanValueParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id => _id;
        private const string _id = "BooleanValue";

        private const string ValueId = "Value";

        public BooleanValueParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true,
                (Lp.One(c => Char.ToLower(c) == 't') + Lp.One(c => Char.ToLower(c) == 'r') + Lp.One(c => Char.ToLower(c) == 'u') + Lp.One(c => Char.ToLower(c) == 'e')).Id(ValueId) |
                (Lp.One(c => Char.ToLower(c) == 'f') + Lp.One(c => Char.ToLower(c) == 'a') + Lp.One(c => Char.ToLower(c) == 'l') + Lp.One(c => Char.ToLower(c) == 's') + Lp.One(c => Char.ToLower(c) == 'e')).Id(ValueId)
            );
        }


        public bool Parse(LpNode node)
        {     
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, ValueId).Match.ToString().ToLower();
            return text == "true" ? true : false;
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
