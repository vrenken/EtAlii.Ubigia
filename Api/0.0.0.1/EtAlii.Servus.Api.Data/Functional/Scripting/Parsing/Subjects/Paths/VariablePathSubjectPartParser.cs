namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class VariablePathSubjectPartParser : IPathSubjectPartParser
    {
        public const string Id = "VariablePathSubjectPart";
        private const string TextId = "Text";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;

        public VariablePathSubjectPartParser(IParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
            _parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(TextId));
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var text = _parserHelper.FindFirst(node, TextId).Match.ToString();
            return new VariablePathSubjectPart(text);
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is VariablePathSubjectPart;
        }
    }
}
