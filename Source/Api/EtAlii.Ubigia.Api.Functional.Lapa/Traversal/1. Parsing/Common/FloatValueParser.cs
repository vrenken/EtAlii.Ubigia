// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Globalization;
    using Moppet.Lapa;

    internal class FloatValueParser : IFloatValueParser
    {
        private readonly INodeValidator _nodeValidator;

        public LpsParser Parser { get; }

        public string Id { get; } = "FloatValue";

        public FloatValueParser(INodeValidator nodeValidator)
        {
            _nodeValidator = nodeValidator;
            Parser = new LpsParser(Id, true, Lp.One(c => c == '-' || c == '+').Maybe() + Lp.OneOrMore(c => char.IsDigit(c)) + Lp.One(c => c == '.') + Lp.OneOrMore(c => char.IsDigit(c)));
        }


        public float Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = node.Match.ToString();
            return float.Parse(text, CultureInfo.InvariantCulture); //TODO: we need to ensure the . is always used as separator.
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
