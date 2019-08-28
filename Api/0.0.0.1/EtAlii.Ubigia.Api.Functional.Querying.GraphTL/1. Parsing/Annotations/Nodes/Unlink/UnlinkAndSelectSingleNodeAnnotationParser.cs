namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal class UnlinkAndSelectSingleNodeAnnotationParser : IUnlinkAndSelectSingleNodeAnnotationParser
    {
        public string Id { get; } = nameof(UnlinkAndSelectSingleNodeAnnotation);
        public LpsParser Parser { get; }
        
        private const string ContentId = "Content";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        public UnlinkAndSelectSingleNodeAnnotationParser(INodeValidator nodeValidator, INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            var content = new LpsParser(ContentId, true, Lp.LetterOrDigit().OneOrMore()); 

            Parser = new LpsParser(Id, true, AnnotationPrefix.NodeUnlink + "(" + content.Maybe() + ")");
        }
        
        public AnnotationNew Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var contentNode = _nodeFinder.FindFirst(node, ContentId);
            
            return new UnlinkAndSelectSingleNodeAnnotation(null, null, null);
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
            return annotation is UnlinkAndSelectSingleNodeAnnotation;
        }
    }
}