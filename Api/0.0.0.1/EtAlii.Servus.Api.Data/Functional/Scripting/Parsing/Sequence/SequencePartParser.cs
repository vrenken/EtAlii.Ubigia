namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    internal class SequencePartParser
    {
        public const string Id = "SequencePart";
        public readonly LpsParser Parser;
        private readonly IParserHelper _parserHelper;
        private readonly IEnumerable<ISequencePartParser> _parsers;

        public SequencePartParser(IParserHelper parserHelper, IEnumerable<ISequencePartParser> parsers)
        {
            _parserHelper = parserHelper;
            _parsers = parsers;
            var lpsParsers = new LpsAlternatives();
            foreach (var parser in _parsers)
            {
                lpsParsers |= parser.Parser;
            }
            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public SequencePart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var childNode = node.Children.Single();
            
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);

            return result;
        }

        public void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after)
        {
            var parser = _parsers.Single(p => p.CanValidate(part));
            parser.Validate(before, part, partIndex, after);
        }
    }
}
