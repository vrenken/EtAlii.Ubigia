namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Data.Model;
    using Moppet.Lapa;

    internal class CommentParser : ISequencePartParser
    {
        public const string Id = "Comment";
        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private const string TextId = "Text";

        public CommentParser(IParserHelper parserHelper)

        {
            _parserHelper = parserHelper;
            _parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Char('#') + Lp.ZeroOrMore(c => true).Id(TextId));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var text = _parserHelper.FindFirst(node, TextId).Match.ToString();
            return new Comment(text);
        }

        public bool CanValidate(SequencePart part)
        {
            return part is Comment;
        }

        public void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after)
        {
            //throw new System.NotImplementedException();
        }
    }
}
