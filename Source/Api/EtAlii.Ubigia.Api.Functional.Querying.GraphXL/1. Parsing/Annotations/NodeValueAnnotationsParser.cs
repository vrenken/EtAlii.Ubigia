namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class NodeValueAnnotationsParser : INodeValueAnnotationsParser
    {
        public string Id { get; } = nameof(NodeValueAnnotation);

        public LpsParser Parser { get; }

        private readonly INodeValueAnnotationParser[] _parsers;
        private readonly INodeValidator _nodeValidator;

        public NodeValueAnnotationsParser(
            INodeValidator nodeValidator,
            ISetAndSelectNodeValueAnnotationParser setAndSelectNodeValueAnnotationParser,
            IClearAndSelectNodeValueAnnotationParser clearAndSelectNodeValueAnnotationParser,
            ISelectNodeValueAnnotationParser selectNodeValueAnnotationParser)
        {
            _parsers = new INodeValueAnnotationParser[]
            {
                setAndSelectNodeValueAnnotationParser,
                clearAndSelectNodeValueAnnotationParser,
                selectNodeValueAnnotationParser
            };

            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public NodeValueAnnotation Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }

        public bool CanParse(LpNode node)
        {
            throw new System.NotImplementedException();
        }

        public void Validate(StructureFragment parent, StructureFragment self, NodeValueAnnotation annotation, int depth)
        {
            //var subject = (NodeAnnotation)self
            var parser = _parsers.Single(p => p.CanValidate(annotation));
            parser.Validate(parent, self, annotation, depth);

//            if [before is Subject || after is Subject]
//            [
//                throw new ScriptParserException["Two subjects cannot be combined."]
//            ]
//            if (before is Comment)
//            [
//                throw new ScriptParserException["A subject cannot used in combination with comments."]
//            ]
        }

        public bool CanValidate(Annotation annotation)
        {
            return annotation is NodeValueAnnotation;
        }
    }
}
