namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Linq;
    using System;

    internal class OperatorParser : ISequencePartParser
    {
        public const string Id = "Operator";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly IEnumerable<IOperatorParser> _parsers;

        public OperatorParser(
            IParserHelper parserHelper,
            IEnumerable<IOperatorParser> parsers)
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
            return part is Operator;
        }

        public void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after)
        {
            var @operator = (Operator)part;
            var parser = _parsers.Single(p => p.CanValidate(@operator));
            parser.Validate(before, @operator, partIndex, after);

            if (before is Operator || after is Operator)
            {
                throw new ScriptParserException("Two operators cannot be combined.");
            }
            if(before is Comment)
            {
                throw new ScriptParserException("A operator cannot used in combination with comments.");
            }
        }

    }
}
