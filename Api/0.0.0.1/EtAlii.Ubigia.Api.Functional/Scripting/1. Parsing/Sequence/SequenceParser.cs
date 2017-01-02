namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class SequenceParser : ISequenceParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "Sequence";
        private readonly ISequencePartsParser _sequencePartsParser;

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        
        private readonly INodeValidator _nodeValidator;

        public SequenceParser(
            INodeValidator nodeValidator,
            ISequencePartsParser sequencePartsParser)
        {
            _nodeValidator = nodeValidator;
            _sequencePartsParser = sequencePartsParser;

            _parser = new LpsParser(Id, true, _sequencePartsParser.Parser.OneOrMore()); 
        }

        public Sequence Parse(string text)
        {
            var node = _parser.Do(text);
            _nodeValidator.EnsureSuccess(node, Id, false);
            var childNodes = node.Children ?? new LpNode[] { };
            var parts = childNodes
                .Select(childNode => _sequencePartsParser.Parse(childNode))
                .ToList();

            for (int i = 0; i < parts.Count; i++)
            {
                var before = i > 0 ? parts[i - 1] : null;
                var after = i < parts.Count - 1 ? parts[i + 1] : null;
                var part = parts[i];
                _sequencePartsParser.Validate(before, part, i, after);
            }

            // If the first part of the sequence is a subject we add an additional assignment operator (<=) to output the result.
            //if (parts.First() is Subject)
            if (!parts.Any(p => p is Operator) && 
                !(parts.Count == 1 && parts.First() is Comment))
            {
                parts.Insert(0, new AssignOperator());
            }

            var sequence = new Sequence(parts.ToArray());
            return sequence;
        }
    }
}
