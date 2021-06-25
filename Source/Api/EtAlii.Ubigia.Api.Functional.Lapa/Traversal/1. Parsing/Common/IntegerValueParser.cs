// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class IntegerValueParser : IIntegerValueParser
    {
        private readonly INodeValidator _nodeValidator;

        public LpsParser Parser { get; }

        string IIntegerValueParser.Id => Id;
        public const string Id = "IntegerValue";

        public IntegerValueParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.One(c => c == '-' || c == '+').Maybe() + Lp.OneOrMore(c => char.IsDigit(c)));
        }


        public int Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = node.Match.ToString();
            return int.Parse(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
