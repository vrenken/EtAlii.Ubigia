namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;
    using System.Linq;

    internal class SequenceParser
    {
        private const string Id = "Sequence";
        private readonly SequencePartParser _sequencePartParser;
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;

        public SequenceParser(
            IParserHelper parserHelper,
            SequencePartParser sequencePartParser)
        {
            _parserHelper = parserHelper;
            _sequencePartParser = sequencePartParser;

            _parser = new LpsParser(Id, true, _sequencePartParser.Parser.OneOrMore()); 
        }

        public Sequence Parse(string text)
        {
            var node = _parser.Do(text);
            _parserHelper.EnsureSuccess2(node, Id, false);
            var childNodes = node.Children ?? new LpNode[] { };
            var parts = childNodes.Select(childNode => _sequencePartParser.Parse(childNode)).ToList();

            for (int i = 0; i < parts.Count; i++)
            {
                var before = i > 0 ? parts[i - 1] : null;
                var after = i < parts.Count - 1 ? parts[i + 1] : null;
                var part = parts[i];
                _sequencePartParser.Validate(before, part, i, after);
            }

            // If the first part of the sequence is a subject we add an additional assignment operator (<=) to output the result.
            //if (parts.First() is Subject)
            if (!parts.Any(p => p is Operator) && !(parts.Count == 1 && parts.First() is Comment))
            {
                parts.Insert(0, new AssignOperator());
            }

            var sequence = new Sequence(parts);
            return sequence;
        }
    }
}
