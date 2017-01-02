namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System;
    using System.Linq;

    internal class StringConstantSubjectParser : IConstantSubjectParser
    {
        public const string Id = "StringConstantSubject";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly IConstantHelper _constantHelper;
        private const string TextId = "Text";

        public StringConstantSubjectParser(
            IParserHelper parserHelper,
            IConstantHelper constantHelper)
        {
            _parserHelper = parserHelper;
            _constantHelper = constantHelper;

            _parser = new LpsParser(Id, true,
                (Lp.One(c => c == '\"') + Lp.ZeroOrMore(c => _constantHelper.IsValidQuotedConstantCharacter(c) || c == '\'').Id(TextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.ZeroOrMore(c => _constantHelper.IsValidQuotedConstantCharacter(c) || c == '\"').Id(TextId) + Lp.One(c => c == '\''))
            );
        }

        public ConstantSubject Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var text = _parserHelper.FindFirst(node, TextId).Match.ToString();
            return new StringConstantSubject(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after)
        {
        }

        public bool CanValidate(ConstantSubject constantSubject)
        {
            return constantSubject is StringConstantSubject;
        }
    }
}
