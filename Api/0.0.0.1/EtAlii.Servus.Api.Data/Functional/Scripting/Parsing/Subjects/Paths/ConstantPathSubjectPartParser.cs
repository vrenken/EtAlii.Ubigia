namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class ConstantPathSubjectPartParser : IPathSubjectPartParser
    {
        public const string Id = "ConstantPathSubjectPart";
        private const string TextId = "Text";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly IConstantHelper _constantHelper;

        public ConstantPathSubjectPartParser(
            IParserHelper parserHelper,
            IConstantHelper constantHelper)
        {
            _parserHelper = parserHelper;
            _constantHelper = constantHelper;

            _parser = new LpsParser(Id, true,
                (Lp.One(c => _constantHelper.IsValidConstantCharacter(c)).OneOrMore().Id(TextId)) |
                (Lp.One(c => c == '\"') + Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c) || c == '\'').OneOrMore().Id(TextId) + Lp.One(c => c == '\"')) |
                (Lp.One(c => c == '\'') + Lp.One(c => _constantHelper.IsValidQuotedConstantCharacter(c) || c == '"').OneOrMore().Id(TextId) + Lp.One(c => c == '\'')) 
            );
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var text = _parserHelper.FindFirst(node, TextId).Match.ToString();
            return new ConstantPathSubjectPart(text);
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (before is ConstantPathSubjectPart || after is ConstantPathSubjectPart)
            {
                throw new ScriptParserException("Two constant path parts cannot be combined.");
            }
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is ConstantPathSubjectPart;
        }
    }
}
