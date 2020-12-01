﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal class BooleanValueParser : IBooleanValueParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = "BooleanValue";

        private const string _valueId = "Value";

        public BooleanValueParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true,
                (Lp.One(c => char.ToLower(c) == 't') + Lp.One(c => char.ToLower(c) == 'r') + Lp.One(c => char.ToLower(c) == 'u') + Lp.One(c => char.ToLower(c) == 'e')).Id(_valueId) |
                (Lp.One(c => char.ToLower(c) == 'f') + Lp.One(c => char.ToLower(c) == 'a') + Lp.One(c => char.ToLower(c) == 'l') + Lp.One(c => char.ToLower(c) == 's') + Lp.One(c => char.ToLower(c) == 'e')).Id(_valueId)
            );
        }


        public bool Parse(LpNode node)
        {     
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _valueId).Match.ToString().ToLower();
            return text == "true";
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
