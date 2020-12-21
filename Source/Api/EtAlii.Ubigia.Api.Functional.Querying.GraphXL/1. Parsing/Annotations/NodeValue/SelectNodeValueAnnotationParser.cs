namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal class SelectNodeValueAnnotationParser : ISelectNodeValueAnnotationParser
    {
        public string Id { get; } = nameof(SelectNodeValueAnnotation);
        public LpsParser Parser { get; }
        
        private const string _sourceId = "Source";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;

        public SelectNodeValueAnnotationParser(
            INodeValidator nodeValidator, 
            INodeFinder nodeFinder, 
            INonRootedPathSubjectParser nonRootedPathSubjectParser, 
            IRootedPathSubjectParser rootedPathSubjectParser,
            IWhitespaceParser whitespaceParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;

            // @value(SOURCE)
            // @value()
            var sourceParser = new LpsParser(_sourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);
            
            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.NodeValue + "(" + whitespaceParser.Optional + sourceParser.Maybe() + whitespaceParser.Optional + ")");
        }

        public NodeValueAnnotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            PathSubject sourcePath = null;
            var sourceNode = _nodeFinder.FindFirst(node, _sourceId);
            if(sourceNode != null)
            {
                var sourceChildNode = sourceNode.Children.Single();
                sourcePath = sourceChildNode.Id switch
                {
                    { } id when id == _rootedPathSubjectParser.Id => (PathSubject) _rootedPathSubjectParser.Parse(sourceChildNode),
                    { } id when id == _nonRootedPathSubjectParser.Id => (PathSubject) _nonRootedPathSubjectParser.Parse(sourceChildNode),
                    _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
                };
            }
            return new SelectNodeValueAnnotation(sourcePath);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(StructureFragment parent, StructureFragment self, NodeValueAnnotation annotation, int depth)
        {
            throw new NotImplementedException();
        }

        public bool CanValidate(NodeValueAnnotation annotation)
        {
            return annotation is SelectNodeValueAnnotation;
        }
    }
}
