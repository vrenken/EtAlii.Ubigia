namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class CommentParser : ICommentParser
    {
        public string Id { get; } = nameof(Comment);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public CommentParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)

        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Char('#') + Lp.ZeroOrMore(c => c != '\n').Id(TextId));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
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
