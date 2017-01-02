namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System;

    internal class SubjectParser : ISequencePartParser
    {
        public const string Id = "Subject";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly IEnumerable<ISubjectParser> _parsers;

        public SubjectParser(IParserHelper parserHelper, IEnumerable<ISubjectParser> parsers)
        {
            _parserHelper = parserHelper;
            _parsers = parsers;
            var lpsParsers = new LpsAlternatives();
            foreach (var parser in _parsers)
            {
                lpsParsers |= parser.Parser;
            }
            _parser = new LpsParser(Id, true, lpsParsers);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }

        public bool CanValidate(SequencePart part)
        {
            return part is Subject;
        }

        public void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after)
        {
            var subject = (Subject)part;
            var parser = _parsers.Single(p => p.CanValidate(subject));
            parser.Validate(before, subject, partIndex, after);

            if (before is Subject || after is Subject)
            {
                throw new ScriptParserException("Two subjects cannot be combined.");
            }
            if (before is Comment)
            {
                throw new ScriptParserException("A subject cannot used in combination with comments.");
            }
        }
    }
}
