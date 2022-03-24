// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    internal sealed class CommentParser : ICommentParser
    {
        public string Id => nameof(Comment);

        public LpsParser Parser { get; }

        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public CommentParser(INodeFinder nodeFinder)
        {
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Char('-') + Lp.Char('-') + Lp.ZeroOrMore(c => c != '\n').Id(TextId));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node, INodeValidator nodeValidator)
        {
            nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            return new Comment(text);
        }
    }
}
