﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Scripting;
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

        public AnnotationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser,
            IOperatorsParser operatorsParser,
            ISubjectsParser subjectsParser, 
            IWhitespaceParser whitespaceParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;
            _operatorsParser = operatorsParser;
            _subjectsParser = subjectsParser;

            var combinedParser = new LpsParser(CombinedContentId, true, 
                (rootedPathSubjectParser.Parser | nonRootedPathSubjectParser.Parser) + 
                whitespaceParser.Optional + 
                operatorsParser.Parser.Maybe() + 
                whitespaceParser.Optional + 
                subjectsParser.Parser.Maybe()); 
            
            var content = new LpsParser(ContentId, true, 
                combinedParser  | 
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
                    case { } id when id == _rootedPathSubjectParser.Id:
                        targetPath = (PathSubject)_rootedPathSubjectParser.Parse(childNode);
                        break;
                    case { } id when id == _nonRootedPathSubjectParser.Id:
                        targetPath = (PathSubject)_nonRootedPathSubjectParser.Parse(childNode);
                        break;

                    case { } id when id == CombinedContentId:

                        var targetPathNode = childNode.Children.FirstOrDefault();
                        if (_nodeFinder.FindFirst(targetPathNode, _rootedPathSubjectParser.Id) is { } rootedPathNode)
                        {
                            targetPath = (PathSubject) _rootedPathSubjectParser.Parse(rootedPathNode);
                        }
                        else if (_nodeFinder.FindFirst(targetPathNode, _nonRootedPathSubjectParser.Id) is { } nonRootedPathNode)
                        {
                            targetPath = (PathSubject) _nonRootedPathSubjectParser.Parse(nonRootedPathNode);
                        }
                        else
                        {
                            throw new NotSupportedException($"Cannot find path subject in: {node.Match}");
                        }

                        var skippedChildren = childNode.Children.Skip(1).ToArray();
                        if (_nodeFinder.FindFirst(skippedChildren, _operatorsParser.Id) is { } operatorNode)
                        {
                            @operator = (Operator) _operatorsParser.Parse(operatorNode);
                            
                            if (_nodeFinder.FindFirst(skippedChildren, _subjectsParser.Id) is { } subjectNode)
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
