﻿namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class CommentParser : ICommentParser
    {
        public string Id => _id;
        private readonly string _id = "Comment";
        
        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;
        
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

        public CommentParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)

        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _parser = new LpsParser(Id, true, Lp.ZeroOrMore(' ') + Lp.Char('#') + Lp.ZeroOrMore(c => c != '\n').Id(_textId));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public SequencePart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
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
