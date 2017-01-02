namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System;
    using System.Linq;

    internal class ConstantSubjectParser : ISubjectParser
    {
        public const string Id = "ConstantSubject";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly IEnumerable<IConstantSubjectParser> _parsers;
        private const string TextId = "Text";

        public ConstantSubjectParser(IParserHelper parserHelper, IEnumerable<IConstantSubjectParser> parsers)
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

        public Subject Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            var constantSubject = (ConstantSubject)subject;
            var parser = _parsers.Single(p => p.CanValidate(constantSubject));
            parser.Validate(before, constantSubject, subjectIndex, after);
        }

        public bool CanValidate(Subject subject)
        {
            return subject is ConstantSubject;
        }
    }
}
