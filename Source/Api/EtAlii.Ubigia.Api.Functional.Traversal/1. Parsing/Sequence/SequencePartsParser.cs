namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class SequencePartsParser : ISequencePartsParser
    {
        public string Id { get; } = "SequenceParts";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly ISequencePartParser[] _parsers;

        public SequencePartsParser(
            IOperatorsParser operatorsParser,
            ISubjectsParser subjectsParser,
            ICommentParser commentParser,
            INodeValidator nodeValidator)
        {
            _parsers = new ISequencePartParser[]
            {
                operatorsParser,
                subjectsParser,
                commentParser
            };

            _nodeValidator = nodeValidator;

            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public SequencePart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNode = node.Children.Single();

            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);

            return result;
        }

        public void Validate(SequencePart before, SequencePart part, int partIndex, SequencePart after)
        {
            var parser = _parsers.Single(p => p.CanValidate(part));
            parser.Validate(before, part, partIndex, after);
        }
    }
}
