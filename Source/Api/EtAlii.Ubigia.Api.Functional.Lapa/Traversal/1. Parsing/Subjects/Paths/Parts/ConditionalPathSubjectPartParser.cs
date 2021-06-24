// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ConditionalPathSubjectPartParser : IConditionalPathSubjectPartParser
    {
        public string Id => nameof(ConditionalPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IConditionParser _conditionParser;
        private readonly INodeFinder _nodeFinder;

        public ConditionalPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConditionParser conditionParser,
            INewLineParser newLineParser,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _conditionParser = conditionParser;
            _nodeFinder = nodeFinder;

            var separator = Lp.Char('&');//.Debug("Separator", true)
            Parser = new LpsParser(Id, true,
                Lp.One(c => c == '.') + //.Debug("Point") +
                newLineParser.OptionalMultiple + //.Debug("NL1") +
                Lp.List(_conditionParser.Parser, separator, newLineParser.OptionalMultiple));
                //Lp.List(_conditionParser.Parser.Debug("Kvp", true), separator, _newLineParser.OptionalMultiple.Debug("NL4")))
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var conditions = _nodeFinder
                .FindAll(node, _conditionParser.Id)
                .Select(n => _conditionParser.Parse(n))
                .ToArray();

            return new ConditionalPathSubjectPart(conditions);
        }
    }
}
