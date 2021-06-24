// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ObjectConstantSubjectParser : IObjectConstantSubjectParser
    {
        public string Id => nameof(ObjectConstantSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IKeyValuePairParser _keyValuePairParser;

        public ObjectConstantSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            IKeyValuePairParser keyValuePairParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _keyValuePairParser = keyValuePairParser;

            var start = Lp.One(c => c == '{'); //.Debug("StartBracket")
            var end = Lp.One(c => c == '}'); //.Debug("EndBracket")

            var separator = (Lp.ZeroOrMore(' ') + Lp.Char(',') + newLineParser.OptionalMultiple);//; //.Debug("Comma")
            Parser = new LpsParser(Id, true,
                Lp.InBrackets(
                start,
                newLineParser.OptionalMultiple +
                Lp.List(_keyValuePairParser.Parser, separator, Lp.ZeroOrMore(' ')).Maybe() + newLineParser.OptionalMultiple,
                end)
                );//.Debug("ObjectConstant")
        }

        public ConstantSubject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var keyValuePairs = _nodeFinder
                .FindAll(node, _keyValuePairParser.Id)
                .Select(n => _keyValuePairParser.Parse(n));

            var dictionary = (IPropertyDictionary)new PropertyDictionary();
            foreach (var kvp in keyValuePairs)
            {
                dictionary.Add(kvp);
            }
            return new ObjectConstantSubject(dictionary);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
