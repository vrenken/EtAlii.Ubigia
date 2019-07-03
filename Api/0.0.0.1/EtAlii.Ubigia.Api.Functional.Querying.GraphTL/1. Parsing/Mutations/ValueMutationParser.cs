namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ValueMutationParser : IValueMutationParser
    {
        private readonly IKeyValuePairParser _keyValuePairParser;
        private readonly IAssignmentParser _assignmentParser;
        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IAnnotationParser _annotationParser;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = nameof(ValueMutation);

        public const string KeyAnnotationId = "KeyAnnotation";
        public const string AnnotationId = "Annotation";
        public const string KeyId = "Key";
        
        public ValueMutationParser(
            INodeValidator nodeValidator,
            IQuotedTextParser quotedTextParser,
            IAnnotationParser annotationParser,
            INodeFinder nodeFinder, 
            IKeyValuePairParser keyValuePairParser,
            IAssignmentParser assignmentParser
            )
        {
            _nodeValidator = nodeValidator;
            _quotedTextParser = quotedTextParser;
            _annotationParser = annotationParser;
            _nodeFinder = nodeFinder;
            _keyValuePairParser = keyValuePairParser;
            _assignmentParser = assignmentParser;

            var keyAnnotationParser = new LpsParser(KeyAnnotationId, true,
         (
                    Lp.Name().Id(KeyId) | _quotedTextParser.Parser.Wrap(KeyId)
                ) +
                _assignmentParser.Parser +
                new LpsParser(AnnotationId, true, annotationParser.Parser));

            var keyValueParser = _keyValuePairParser.Parser; 
            
            Parser = new LpsParser(Id, true, (keyAnnotationParser | keyValueParser));
        }

        public ValueMutation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var child = _nodeFinder.FindFirst(node, Id).Children.Single();
            
            if (child.Id == KeyAnnotationId)
            {
                var keyNode = _nodeFinder.FindFirst(child, KeyId);
                var constantNode = keyNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
                var key = constantNode == null ? keyNode.Match.ToString() : _quotedTextParser.Parse(constantNode);

                var annotationNode = _nodeFinder.FindFirst(child, AnnotationId);
                Annotation annotation = null;
                if (annotationNode != null)
                {
                    var annotationValueNode = _nodeFinder.FindFirst(annotationNode, _annotationParser.Id);
                    annotation = _annotationParser.Parse(annotationValueNode);
//                    if (annotation != Annotation.None && annotation.Type != AnnotationType.Value)
//                    {
//                        throw new QueryParserException("A constant assignment can only be applied to type-annotated elements");
//                    }
                }

                return new ValueMutation(key, annotation, null);            
            }
            else if (child.Id == _keyValuePairParser.Id)
            {
                var kvpNode = _nodeFinder.FindFirst(child, _keyValuePairParser.Id);
                var kvp = _keyValuePairParser.Parse(kvpNode);
                return new ValueMutation(kvp.Key, Annotation.None, kvp.Value);            
            }
            else
            {
                throw new QueryParserException($"Unable to find correctly formatted {nameof(ValueMutation)}.");
            }
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
