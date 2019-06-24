namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class AnnotationParser : IAnnotationParser
    {
        public string Id { get; } = nameof(Annotation);
        private const string AnnotationTypeId = "AnnotationType";
        private const string PathId = "Path";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        private readonly Type _annotationTypeType = typeof(AnnotationType);
        private readonly ISubjectParser[] _pathParsers;

        public AnnotationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            _pathParsers = new ISubjectParser[]
            {
                rootedPathSubjectParser,
                nonRootedPathSubjectParser
            };
             var paths = new LpsParser(PathId, true, _pathParsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser)); 
            // var paths = (rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser);
            //var paths = (rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser).Id(PathId, true);
                
            var annotationTypeConstants = Enum
                .GetNames(_annotationTypeType)
                .Select(v => v.ToLower())
                .OrderByDescending(v => v.Length) // Node should be checked after Nodes.
                .ToArray();

            var annotationTypes = annotationTypeConstants
                .Select(Lp.Term)
                .Aggregate(new LpsAlternatives(), (current, parser) => current | parser)
                .Id(AnnotationTypeId);

            Parser = new LpsParser(Id, true, Lp.Char('@') + annotationTypes + Lp.Char('(') + paths.Maybe() + Lp.Char(')'));
        }

        public Annotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var annotationType = AnnotationType.Nodes;
            var result = _nodeFinder.FindFirst(node, AnnotationTypeId);
            if (result != null)
            {
                var annotationTypeText = result.Match.ToString();
                annotationType = (AnnotationType)Enum.Parse(_annotationTypeType, annotationTypeText, true);
            }

            PathSubject path = null;
            var pathNode = _nodeFinder.FindFirst(node, PathId);
            if (pathNode != null)
            {
                var childNode = pathNode.Children.Single();
                var parser = _pathParsers.Single(p => p.CanParse(childNode));
                path = (PathSubject)parser.Parse(childNode);
            }
            
            return new Annotation(annotationType, path);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after)
        {
        }

        public bool CanValidate(ConstantSubject constantSubject)
        {
            return constantSubject is ObjectConstantSubject;
        }
    }
}
