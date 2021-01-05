namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class SequenceParser : ISequenceParser
    {
        public string Id { get; } = "Sequence";

        private readonly ISequencePartsParser _sequencePartsParser;

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;

        public SequenceParser(
            INodeValidator nodeValidator,
            ISequencePartsParser sequencePartsParser)
        {
            _nodeValidator = nodeValidator;
            _sequencePartsParser = sequencePartsParser;

            Parser = new LpsParser(Id, true, _sequencePartsParser.Parser.OneOrMore());
        }

        public Sequence Parse(string text)
        {
            var node = Parser.Do(text);
            return Parse(node, false);
        }

        public Sequence Parse(LpNode node, bool restIsAllowed)
        {
            _nodeValidator.EnsureSuccess(node, Id, restIsAllowed);
            var childNodes = node.Children ?? Array.Empty<LpNode>();
            var parts = childNodes
                .Select(childNode => _sequencePartsParser.Parse(childNode))
                .ToList();

            // if the first part of the sequence is a subject we add an additional assignment operator [<=] to output the result.
            //if [parts.First[] is Subject]
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
