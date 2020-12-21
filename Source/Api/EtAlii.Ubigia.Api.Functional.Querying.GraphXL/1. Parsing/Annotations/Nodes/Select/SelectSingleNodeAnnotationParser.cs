namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class SelectSingleNodeAnnotationParser : ISelectSingleNodeAnnotationParser
    {
        public string Id { get; } = nameof(SelectSingleNodeAnnotation);
        public LpsParser Parser { get; }

        private const string _sourceId = "Source";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;

        public SelectSingleNodeAnnotationParser(
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

            // @node(SOURCE)
            var sourceParser = new LpsParser(_sourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);

            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.Node + "(" + whitespaceParser.Optional + sourceParser + whitespaceParser.Optional + ")");
        }

        public NodeAnnotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var sourceNode = _nodeFinder.FindFirst(node, _sourceId);
            var sourceChildNode = sourceNode.Children.Single();
            var sourcePath = sourceChildNode.Id switch
            {
                { } id when id == _rootedPathSubjectParser.Id => (PathSubject) _rootedPathSubjectParser.Parse(sourceChildNode),
                { } id when id == _nonRootedPathSubjectParser.Id => (PathSubject) _nonRootedPathSubjectParser.Parse(sourceChildNode),
                _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
            };
            return new SelectSingleNodeAnnotation(sourcePath);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(StructureFragment parent, StructureFragment self, NodeAnnotation annotation, int depth)
        {
            throw new NotImplementedException();
        }

        public bool CanValidate(NodeAnnotation annotation)
        {
            return annotation is SelectSingleNodeAnnotation;
        }
    }
}
