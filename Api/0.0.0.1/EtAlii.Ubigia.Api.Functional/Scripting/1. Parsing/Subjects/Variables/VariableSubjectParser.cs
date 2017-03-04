namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class VariableSubjectParser : IVariableSubjectParser
    {
        public string Id => _id;
        private readonly string _id = "VariableSubject";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;
        
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private const string TextId = "Text";

        public VariableSubjectParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _parser = new LpsParser(Id, true, Lp.Char('$') + Lp.LetterOrDigit().OneOrMore().Id(TextId));
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
        }

        public bool CanValidate(Subject subject)
        {
            return subject is VariableSubject;
        }
    }
}
