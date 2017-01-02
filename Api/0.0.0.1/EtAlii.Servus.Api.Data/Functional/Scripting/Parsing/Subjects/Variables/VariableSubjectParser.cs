namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;

    internal class VariableSubjectParser : ISubjectParser
    {
        public const string Id = "VariableSubject";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private const string TextId = "Text";

        public VariableSubjectParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
            _parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(TextId));
        }

        public Subject Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var text = _parserHelper.FindFirst(node, TextId).Match.ToString();
            return new VariableSubject(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
        }

        public bool CanValidate(Subject subject)
        {
            return subject is VariableSubject;
        }
    }
}
