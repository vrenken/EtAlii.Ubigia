namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class NodeValueFragmentParser : INodeValueFragmentParser
    {
        private readonly IKeyValuePairParser _keyValuePairParser;
        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly INodeValueAnnotationsParser _annotationParser;
        private readonly IRequirementParser _requirementParser;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = "ValueQuery";

        private const string _keyId = "Key";
        private const string _annotationId = "Annotation";
        private const string _queryValueId = "QueryValue";

        public NodeValueFragmentParser(
            INodeValidator nodeValidator,
            IQuotedTextParser quotedTextParser,
            INodeValueAnnotationsParser annotationParser,
            INodeFinder nodeFinder,
            IKeyValuePairParser keyValuePairParser,
            IRequirementParser requirementParser,
            IWhitespaceParser whitespaceParser)
        {
            _nodeValidator = nodeValidator;
            _quotedTextParser = quotedTextParser;
            _annotationParser = annotationParser;
            _nodeFinder = nodeFinder;
            _keyValuePairParser = keyValuePairParser;
            _requirementParser = requirementParser;

            var queryParser = new LpsParser(_queryValueId, true, _requirementParser.Parser + (Lp.Name().Id(_keyId) | _quotedTextParser.Parser.Wrap(_keyId)) + new LpsParser(_annotationId, true, whitespaceParser.Required + annotationParser.Parser).Maybe());

            var mutationKeyValueParser = _keyValuePairParser.Parser;

            Parser = new LpsParser(Id, true, mutationKeyValueParser | queryParser);

        }

        public ValueFragment Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var child = _nodeFinder.FindFirst(node, Id).Children.Single();

            switch (child.Id)
            {
                case _queryValueId:
                    return ParseQueryValue(child);
                case KeyValuePairParser.Id:
                    var kvpNode = _nodeFinder.FindFirst(child, _keyValuePairParser.Id);
                    var kvp = _keyValuePairParser.Parse(kvpNode);
                    return new ValueFragment(kvp.Key, null, Requirement.None, FragmentType.Mutation, kvp.Value);
                default:
                    throw new SchemaParserException($"Unable to find correctly formatted {nameof(ValueFragment)}.");
            }
        }

        private ValueFragment ParseQueryValue(LpNode node)
        {
            var requirementNode = _nodeFinder.FindFirst(node, _requirementParser.Id);
            var requirement = requirementNode != null ? _requirementParser.Parse(requirementNode) : Requirement.None;

            var nameNode = _nodeFinder.FindFirst(node, _keyId);
            var constantNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = constantNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(constantNode);

            var annotationNode = _nodeFinder.FindFirst(node, _annotationId);
            NodeValueAnnotation annotation = null;
            if (annotationNode != null)
            {
                var annotationValueNode = _nodeFinder.FindFirst(annotationNode, _annotationParser.Id);
                annotation = _annotationParser.Parse(annotationValueNode);
            }

            var fragmentType = annotation == null || annotation is SelectNodeValueAnnotation
                ? FragmentType.Query
                : FragmentType.Mutation;
            return new ValueFragment(name, annotation, requirement, fragmentType, null);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
