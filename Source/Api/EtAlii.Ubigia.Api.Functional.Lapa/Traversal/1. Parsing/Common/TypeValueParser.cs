// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal class TypeValueParser : ITypeValueParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id => "TypeValue";

        private const string ValueId = "Value";

        public TypeValueParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            IConstantHelper constantHelper)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            Parser = new LpsParser(Id, true,
                new LpsParser(ValueId, true,
                    Lp.OneOrMore(c => constantHelper.IsValidConstantCharacter(c)) +
                    (Lp.Char('.') + Lp.OneOrMore(c => constantHelper.IsValidConstantCharacter(c))).ZeroOrMore()
                )
            );//.Debug("TypeValueParser", true)
        }


        public string Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var type = _nodeFinder.FindFirst(node, ValueId).Match.ToString();
            return type;
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
