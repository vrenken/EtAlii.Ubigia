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
        private const string CombinedContentId = "CombinedContent";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;

        private readonly Type _annotationTypeType = typeof(AnnotationType);
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly IOperatorsParser _operatorsParser;
        private readonly ISubjectsParser _subjectsParser;
        //private readonly IParser[] _contentParsers;
        private readonly LpsParser _combinedParser;

        public AnnotationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser,
            IOperatorsParser operatorsParser,
            ISubjectsParser subjectsParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;
            _operatorsParser = operatorsParser;
            _subjectsParser = subjectsParser;

            var whitespace = Lp.ZeroOrMore(c => c == ' ' || c == '\t' || c == '\r');
                
            _combinedParser = new LpsParser(CombinedContentId, true, 
                (rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser) + 
                whitespace.Maybe() + 
                operatorsParser.Parser.Maybe() + 
                whitespace.Maybe() + 
                subjectsParser.Parser.Maybe()); 
            
            var content = new LpsParser(ContentId, true, 
                _combinedParser  | 
                _rootedPathSubjectParser.Parser | 
                _nonRootedPathSubjectParser.Parser); 

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

                    case string id when id == CombinedContentId:

                        if (_nodeFinder.FindFirst(childNode.Children, _rootedPathSubjectParser.Id) is LpNode rootedPathNode)
                        {
                            targetPath = (PathSubject) _rootedPathSubjectParser.Parse(rootedPathNode);
                        }
                        else if (_nodeFinder.FindFirst(childNode.Children, _nonRootedPathSubjectParser.Id) is LpNode nonRootedPathNode)
                        {
                            targetPath = (PathSubject) _nonRootedPathSubjectParser.Parse(nonRootedPathNode);
                        }
                        else
                        {
                            throw new NotSupportedException($"Cannot find path subject in: {node.Match}");
                        }

                        if (_nodeFinder.FindFirst(childNode.Children, _operatorsParser.Id) is LpNode operatorNode)
                        {
                            @operator = (Operator) _operatorsParser.Parse(operatorNode);
                            
                            if (_nodeFinder.FindFirst(childNode.Children, _subjectsParser.Id) is LpNode subjectNode)
                            {
                                subject = (Subject) _subjectsParser.Parse(subjectNode);
                            }
                        }
                        break;
                }
            }
            
            return new Annotation(annotationType, targetPath, @operator, subject);
        }
    }
}
