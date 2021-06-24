// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class OperatorsParser : IOperatorsParser
    {
        public string Id { get; } = "Operators";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IOperatorParser[] _parsers;

        public OperatorsParser(
            IAssignOperatorParser assignOperatorParser,
            IAddOperatorParser addOperatorParser,
            IRemoveOperatorParser removeOperatorParser,
            INodeValidator nodeValidator)
        {
            _parsers = new IOperatorParser[]
            {
                assignOperatorParser,
                addOperatorParser,
                removeOperatorParser
            };
            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);//.Debug("OperatorsParser")
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }
    }
}
