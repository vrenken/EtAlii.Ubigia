namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class AnnotationParser : IAnnotationParser
    {
        public string Id { get; } = nameof(Annotation);
        private const string AnnotationTypeId = "AnnotationType";
        private const string ContentId = "Content";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        private readonly Type _annotationTypeType = typeof(AnnotationType);
        private readonly ISequenceParser _sequenceParser;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly IParser[] _contentParsers;
        
        public AnnotationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            ISequenceParser sequenceParser, 
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _sequenceParser = sequenceParser;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;

            _contentParsers = new IParser[]
            {
                rootedPathSubjectParser,
                nonRootedPathSubjectParser,
                sequenceParser
            };
            var content = new LpsParser(ContentId, true, _contentParsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser)); 

            var annotationTypeConstants = Enum
                .GetNames(_annotationTypeType)
                .Select(v => v.ToLower())
                .OrderByDescending(v => v.Length) // Node should be checked after Nodes.
                .ToArray();

            var annotationTypes = annotationTypeConstants
                .Select(Lp.Term)
                .Aggregate(new LpsAlternatives(), (current, parser) => current | parser)
                .Id(AnnotationTypeId);

            Parser = new LpsParser(Id, true, Lp.Char('@') + annotationTypes + Lp.Char('(') + content.Maybe() + Lp.Char(')'));
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

            Operator @operator = null;
            Subject subject = null;
            PathSubject targetPath = null;

            var contentNode = _nodeFinder.FindFirst(node, ContentId);
            if (contentNode != null)
            {
                var childNode = contentNode.Children.Single();

                switch (childNode.Id)
                {
                    case string id when id == _rootedPathSubjectParser.Id:
                        targetPath = (PathSubject)_rootedPathSubjectParser.Parse(childNode);
                        break;
                    case string id when id == _nonRootedPathSubjectParser.Id:
                        targetPath = (PathSubject)_nonRootedPathSubjectParser.Parse(childNode);
                        break;
                    case string id when id == _sequenceParser.Id:
                        var sequence = _sequenceParser.Parse(childNode, true);
                
                        // We don't need the assign operator.
                        var parts = sequence.Parts.SkipWhile(p => p is AssignOperator).ToArray();

                        targetPath = (PathSubject) parts[0];
                        @operator = (Operator) parts[1];
                        subject = (Subject)parts[2];
                        break;
                }
            }
            
            return new Annotation(annotationType, targetPath, @operator, subject);
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
