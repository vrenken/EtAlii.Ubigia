namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class RemoveAndSelectMultipleNodesAnnotationParser : IRemoveAndSelectMultipleNodesAnnotationParser
    {
        public string Id { get; } = nameof(RemoveAndSelectMultipleNodesAnnotation);
        public LpsParser Parser { get; }

        private const string _sourceId = "Source";
        private const string _nameId = "Name";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly IQuotedTextParser _quotedTextParser;

        public RemoveAndSelectMultipleNodesAnnotationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser,
            IWhitespaceParser whitespaceParser,
            IQuotedTextParser quotedTextParser
        )
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;
            _quotedTextParser = quotedTextParser;

            // @nodes-remove(SOURCE, NAME)
            var sourceParser = new LpsParser(_sourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);
            var nameParser = new LpsParser(_nameId, true, Lp.Name().Wrap(_nameId) | _quotedTextParser.Parser);

            Parser = new LpsParser(Id, true, "@" + AnnotationPrefix.NodesRemove + "(" + sourceParser + whitespaceParser.Optional + "," + whitespaceParser.Optional + nameParser + ")");
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

            var nameNode = _nodeFinder.FindFirst(node, _nameId);
            var nameChildNode = nameNode.Children.Single();
            var name = nameChildNode.Id switch
            {
                {} id when id == _quotedTextParser.Id => _quotedTextParser.Parse(nameChildNode),
                _ => nameChildNode.Match.ToString()
            };
            return new RemoveAndSelectMultipleNodesAnnotation(sourcePath, name);
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
            return annotation is RemoveAndSelectMultipleNodesAnnotation;
        }
    }
}
