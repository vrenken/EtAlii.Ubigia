namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class ValueAnnotationsParser : IValueAnnotationsParser
    {
        public string Id => nameof(ValueAnnotation);

        public LpsParser Parser { get; }

        private readonly IValueAnnotationParser[] _parsers;
        private readonly INodeValidator _nodeValidator;

        public ValueAnnotationsParser(
            INodeValidator nodeValidator,
            ISetAndSelectValueAnnotationParser setAndSelectValueAnnotationParser,
            IClearAndSelectValueAnnotationParser clearAndSelectValueAnnotationParser,
            ISelectValueAnnotationParser selectValueAnnotationParser)
        {
            _parsers = new IValueAnnotationParser[]
            {
                setAndSelectValueAnnotationParser,
                clearAndSelectValueAnnotationParser,
                selectValueAnnotationParser
            };

            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public ValueAnnotation Parse(LpNode node)
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

        public void Validate(StructureFragment parent, StructureFragment self, ValueAnnotation annotation, int depth)
        {
            var parser = _parsers.Single(p => p.CanValidate(annotation));
            parser.Validate(parent, self, annotation, depth);
        }

        public bool CanValidate(Annotation annotation)
        {
            return annotation is ValueAnnotation;
        }
    }
}
