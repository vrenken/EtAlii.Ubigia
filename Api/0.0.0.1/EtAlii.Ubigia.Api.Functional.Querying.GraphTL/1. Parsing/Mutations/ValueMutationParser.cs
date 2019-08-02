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

        public string Id { get; } = "ValueMutation";

        private const string KeyAnnotationId = "KeyAnnotation";
        private const string KeyAnnotationValueId = "KeyAnnotationValue";
        private const string KeyId = "Key";
        private const string ValueId = "Value";
        
        public ValueMutationParser(
            INodeValidator nodeValidator,
            IQuotedTextParser quotedTextParser,
            IAnnotationParser annotationParser,
            INodeFinder nodeFinder, 
            IKeyValuePairParser keyValuePairParser,
            IAssignmentParser assignmentParser,
            IWhitespaceParser whitespaceParser)
        {
            _nodeValidator = nodeValidator;
            _quotedTextParser = quotedTextParser;
            _annotationParser = annotationParser;
            _nodeFinder = nodeFinder;
            _keyValuePairParser = keyValuePairParser;
            _assignmentParser = assignmentParser;

            var nameParser = Lp.Name().Id(KeyId) | _quotedTextParser.Parser.Wrap(KeyId);
            var valueParser = Lp.Name().Id(ValueId) | _quotedTextParser.Parser.Wrap(ValueId);
            
            var keyAnnotationParser = new LpsParser(KeyAnnotationId, true, nameParser + _assignmentParser.Parser + annotationParser.Parser);
            var keyAnnotationValueParser = new LpsParser(KeyAnnotationValueId, true, nameParser + whitespaceParser.Required + annotationParser.Parser + _assignmentParser.Parser + valueParser);
            var keyValueParser = _keyValuePairParser.Parser; 
            
            Parser = new LpsParser(Id, true, keyAnnotationParser | keyValueParser | keyAnnotationValueParser);
        }

        public ValueFragment Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var child = _nodeFinder.FindFirst(node, Id).Children.Single();

            switch (child.Id)
            {
                case KeyAnnotationId:
                    var name1 = ParseName(child);
                    var annotation1 = ParseAnnotation(child);
                    return new ValueFragment(name1, annotation1, Requirement.None, FragmentType.Mutation, null);
                case KeyValuePairParser.Id:
                    var kvpNode = _nodeFinder.FindFirst(child, _keyValuePairParser.Id);
                    var kvp = _keyValuePairParser.Parse(kvpNode);
                    return new ValueFragment(kvp.Key, null, Requirement.None, FragmentType.Mutation, kvp.Value);
                case KeyAnnotationValueId:
                    var name2 = ParseName(child);
                    var annotation2 = ParseAnnotation(child);
                    var @value = ParseValue(child);
                    return new ValueFragment(name2, annotation2, Requirement.None, FragmentType.Mutation, value);
                default:
                    throw new SchemaParserException($"Unable to find correctly formatted {nameof(ValueFragment)}.");
            }
        }

        private string ParseValue(LpNode child)
        {
            var keyNode = _nodeFinder.FindFirst(child, ValueId);
            var quotedTextNode = keyNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var value = quotedTextNode == null 
                ? keyNode.Match.ToString() 
                : _quotedTextParser.Parse(quotedTextNode);

            return @value;
        }

        private string ParseName(LpNode child)
        {
            var keyNode = _nodeFinder.FindFirst(child, KeyId);
            var quotedTextNode = keyNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = quotedTextNode == null 
                ? keyNode.Match.ToString() 
                : _quotedTextParser.Parse(quotedTextNode);

            return name;
        }
        private Annotation ParseAnnotation(LpNode node)
        {
            var annotationNode = _nodeFinder.FindFirst(node, _annotationParser.Id);
            if (annotationNode == null)
            {
                throw new SchemaParserException("An annotation could not be found for parsing.");
            }
            var annotation = _annotationParser.Parse(annotationNode);
//          if (annotation != Annotation.None && annotation.Type != AnnotationType.Value)
//          {
//              throw new QueryParserException("A constant assignment can only be applied to type-annotated elements");
//          }

            return annotation;
        }
        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
