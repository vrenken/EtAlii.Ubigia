namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal class AssignAndSelectValueAnnotationParser : IAssignAndSelectValueAnnotationParser
    {
        public string Id { get; } = nameof(AssignAndSelectValueAnnotation);
        public LpsParser Parser { get; }
        
        private const string ContentId = "Content";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;

        public AssignAndSelectValueAnnotationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder, 
            INonRootedPathSubjectParser nonRootedPathSubjectParser, 
            IRootedPathSubjectParser rootedPathSubjectParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;

            // @value-assign(SOURCE, VALUE)

            var content = new LpsParser(ContentId, true, Lp.LetterOrDigit().OneOrMore()); 

            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.ValueAssign + "(" + content.Maybe() + ")");
        }

        public AnnotationNew Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var contentNode = _nodeFinder.FindFirst(node, ContentId);
            
            return new AssignAndSelectValueAnnotation(null, null);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(StructureFragment parent, StructureFragment self, AnnotationNew annotation, int depth)
        {
            throw new System.NotImplementedException();
        }

        public bool CanValidate(AnnotationNew annotation)
        {
            return annotation is AssignAndSelectValueAnnotation;
        }
    }
}