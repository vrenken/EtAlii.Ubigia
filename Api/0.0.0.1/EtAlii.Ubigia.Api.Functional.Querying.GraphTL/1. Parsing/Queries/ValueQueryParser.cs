namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal class ValueQueryParser : IValueQueryParser
    {
        private readonly INodeValidator _nodeValidator;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IAnnotationParser _annotationParser;
        private readonly IRequirementParser _requirementParser;
        private readonly INodeFinder _nodeFinder;

        public LpsParser Parser { get; }

        public string Id { get; } = nameof(ValueQuery);

        private const string KeyId = "Key";
        private const string RequirementId = "Requirement";
        private const string AnnotationId = "Annotation";

        public ValueQueryParser(
            INodeValidator nodeValidator,
            IQuotedTextParser quotedTextParser,
            IAnnotationParser annotationParser,
            INodeFinder nodeFinder, 
            IRequirementParser requirementParser,
            IWhitespaceParser whitespaceParser)
        {
            _nodeValidator = nodeValidator;
            _quotedTextParser = quotedTextParser;
            _annotationParser = annotationParser;
            _nodeFinder = nodeFinder;
            _requirementParser = requirementParser;

            Parser = new LpsParser(Id, true,
                _requirementParser.Parser + 
                (
                    Lp.Name().Id(KeyId) |
                    _quotedTextParser.Parser.Wrap(KeyId)
                ) +
                whitespaceParser.Optional +
                //Lp.Char(':') +
                //Lp.ZeroOrMore(' ') +
                new LpsParser(AnnotationId, true, annotationParser.Parser).Maybe());
        }

        public ValueQuery Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
                
            var requirementNode = _nodeFinder.FindFirst(node, _requirementParser.Id);
            var requirement = requirementNode != null ? _requirementParser.Parse(requirementNode) : Requirement.None;

            var nameNode = _nodeFinder.FindFirst(node, KeyId);
            var constantNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = constantNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(constantNode);

            var annotationNode = _nodeFinder.FindFirst(node, AnnotationId);
            Annotation annotation = null;
            if (annotationNode != null)
            {
                var annotationValueNode = _nodeFinder.FindFirst(annotationNode, _annotationParser.Id);
                annotation = _annotationParser.Parse(annotationValueNode);
            }

            return new ValueQuery(name, annotation, requirement);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }
    }
}
