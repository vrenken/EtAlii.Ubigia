namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ObjectConstantSubjectParser : IObjectConstantSubjectParser
    {
        public string Id { get; } = nameof(ObjectConstantSubject);

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

            var start = Lp.One(c => c == '{'); 
            var end = Lp.One(c => c == '}'); 

            var separator = (Lp.ZeroOrMore(' ') + Lp.Char(',') + newLineParser.OptionalMultiple);
            Parser = new LpsParser(Id, true,
                Lp.InBrackets(
                start,
                newLineParser.OptionalMultiple +
                Lp.List(_keyValuePairParser.Parser, separator, Lp.ZeroOrMore(' ')).Maybe() + newLineParser.OptionalMultiple,
                end)
                );
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

        public void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after)
        {
            // Validate the object constant subject.
        }

        public bool CanValidate(ConstantSubject constantSubject)
        {
            return constantSubject is ObjectConstantSubject;
        }
    }
}
