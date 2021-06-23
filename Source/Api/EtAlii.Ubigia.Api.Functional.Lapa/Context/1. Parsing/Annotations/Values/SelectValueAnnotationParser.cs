namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class SelectValueAnnotationParser : ISelectValueAnnotationParser
    {
        public string Id => nameof(SelectValueAnnotation);
        public LpsParser Parser { get; }

        private const string SourceId = "Source";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;

        public SelectValueAnnotationParser(
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
            var sourceParser = new LpsParser(SourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);

            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.Value + "(" + whitespaceParser.Optional + sourceParser.Maybe() + whitespaceParser.Optional + ")");
        }

        public ValueAnnotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            PathSubject sourcePath = null;
            var sourceNode = _nodeFinder.FindFirst(node, SourceId);
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
            return new SelectValueAnnotation(sourcePath);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(StructureFragment parent, StructureFragment self, ValueAnnotation annotation, int depth)
        {
            throw new NotImplementedException();
        }

        public bool CanValidate(ValueAnnotation annotation)
        {
            return annotation is SelectValueAnnotation;
        }
    }
}
