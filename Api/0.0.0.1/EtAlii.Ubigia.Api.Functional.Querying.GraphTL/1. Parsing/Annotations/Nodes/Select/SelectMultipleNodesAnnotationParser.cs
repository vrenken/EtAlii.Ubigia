namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal class SelectMultipleNodesAnnotationParser : ISelectMultipleNodesAnnotationParser
    {
        public string Id { get; } = nameof(SelectMultipleNodesAnnotation);
        public LpsParser Parser { get; }
        
        private const string ContentId = "Content";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public SelectMultipleNodesAnnotationParser(INodeValidator nodeValidator, INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            
            // @nodes(SOURCE)

            var content = new LpsParser(ContentId, true, Lp.LetterOrDigit().OneOrMore()); 

            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.Nodes + "(" + content.Maybe() + ")");
        }

        public AnnotationNew Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var contentNode = _nodeFinder.FindFirst(node, ContentId);
            
            return new SelectMultipleNodesAnnotation(null);
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
            return annotation is SelectMultipleNodesAnnotation;
        }
    }
}