namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Moppet.Lapa;

    internal class LinkAndSelectSingleNodeAnnotationParser : ILinkAndSelectSingleNodeAnnotationParser
    {
        public string Id { get; } = nameof(LinkAndSelectSingleNodeAnnotation);
        public LpsParser Parser { get; }
        
        private const string SourceId = "Source";
        private const string TargetId = "Target";
        private const string TargetLinkId = "TargetLink";

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;

        public LinkAndSelectSingleNodeAnnotationParser(
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
            
            // @node-link(SOURCE, TARGET, TARGET_LINK)
            var sourceParser = new LpsParser(SourceId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser).Debug("Source", true);
            var targetParser = new LpsParser(TargetId, true, rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser).Debug("Target", true);
            var targetLinkParser = new LpsParser(TargetLinkId, true, nonRootedPathSubjectParser.Parser).Debug("TargetLink", true);
            
            Parser = new LpsParser(Id, true, 
                "@" + AnnotationPrefix.NodeLink + "(" + 
                sourceParser + whitespaceParser.Optional + "," + whitespaceParser.Optional + 
                targetParser + whitespaceParser.Optional + "," + whitespaceParser.Optional +
                targetLinkParser + ")");
        }

        public AnnotationNew Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var sourceNode = _nodeFinder.FindFirst(node, SourceId);
            var sourceChildNode = sourceNode.Children.Single();
            var sourcePath = sourceChildNode.Id switch
            {
                { } id when id == _rootedPathSubjectParser.Id => (PathSubject) _rootedPathSubjectParser.Parse(sourceChildNode),
                { } id when id == _nonRootedPathSubjectParser.Id => (PathSubject) _nonRootedPathSubjectParser.Parse(sourceChildNode),
                _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
            };
            
            var targetNode = _nodeFinder.FindFirst(node, TargetId);
            var targetChildNode = targetNode.Children.Single();
            var targetPath = targetChildNode.Id switch
            {
                { } id when id == _rootedPathSubjectParser.Id => (PathSubject) _rootedPathSubjectParser.Parse(targetChildNode),
                { } id when id == _nonRootedPathSubjectParser.Id => (PathSubject) _nonRootedPathSubjectParser.Parse(targetChildNode),
                _ => throw new NotSupportedException($"Cannot find path subject in: {node.Match}")
            };

            var targetLinkNode = _nodeFinder.FindFirst(node, TargetLinkId);
            var targetLinkChildNode = targetLinkNode.Children.Single();
            NonRootedPathSubject targetLinkPath;
            if (targetLinkChildNode.Id == _nonRootedPathSubjectParser.Id)
            {
                targetLinkPath = _nonRootedPathSubjectParser.Parse(targetLinkChildNode) as NonRootedPathSubject;
                if (targetLinkPath == null)
                {
                    throw new NotSupportedException($"Cannot find non-rooted target link path in: {node.Match}");
                }
            }
            else
            {
                throw new NotSupportedException($"Cannot find non-rooted target link path in: {node.Match}");
            }

            return new LinkAndSelectSingleNodeAnnotation(sourcePath, targetPath, targetLinkPath);
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
            return annotation is LinkAndSelectSingleNodeAnnotation;
        }
    }
}