namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class VariablePathSubjectPartParser : IVariablePathSubjectPartParser
    {
        public string Id { get; } = "VariablePathSubjectPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string _textId = "Text";

        public VariablePathSubjectPartParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(_textId));
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, _textId).Match.ToString();
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
