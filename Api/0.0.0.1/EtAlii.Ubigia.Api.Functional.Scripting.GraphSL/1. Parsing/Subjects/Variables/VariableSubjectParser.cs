namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class VariableSubjectParser : IVariableSubjectParser
    {
        public string Id { get; } = nameof(VariableSubject);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public VariableSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            Parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(TextId));
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, TextId).Match.ToString();
            return new VariableSubject(text);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            // Validate the Subject in combination with the before/after SequencePart combination.
        }

        public bool CanValidate(Subject subject)
        {
            return subject is VariableSubject;
        }
    }
}
