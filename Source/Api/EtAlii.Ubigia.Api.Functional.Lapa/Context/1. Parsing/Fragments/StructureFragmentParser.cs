namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

    internal class StructureFragmentParser : IStructureFragmentParser
    {
        public string Id { get; } = "StructureQuery";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IValueFragmentParser _valueFragmentParser;
        private readonly INodeAnnotationsParser _annotationParser;

        private const string NameId = "NameId";
        private const string FragmentsId = "FragmentsId";
        private const string ChildStructureQueryId = "ChildStructureQuery";
        private const string ChildStructureQueryHeaderId = "ChildStructureQueryHeader";

        public StructureFragmentParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            IQuotedTextParser quotedTextParser,
            IValueFragmentParser valueFragmentParser,
            INodeAnnotationsParser annotationParser,
            IRequirementParser requirementParser,
            IWhitespaceParser whitespaceParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _quotedTextParser = quotedTextParser;
            _valueFragmentParser = valueFragmentParser;
            _annotationParser = annotationParser;

            var start = Lp.One(c => c == '{'); //.Debug("StartBracket")
            var end = Lp.One(c => c == '}'); //.Debug("EndBracket")

            var structureQueryParser = new LpsParser(ChildStructureQueryId);

            var fragmentsParser = new LpsParser(FragmentsId, true,
                structureQueryParser |
                _valueFragmentParser.Parser); //.Debug("VQ", true)

            var whitespace = whitespaceParser.Optional;//.Debug("W")
            var lineSeparator = whitespace + new LpsParser(Lp.Term("\r\n") | Lp.Term("\n")) + whitespace;

            var spaceSeparator = whitespaceParser.Required;
            var commaSeparator = whitespace + new LpsParser((',' + whitespace + '\n') | ',') + whitespace;

            var fragments = new LpsParser(Lp.List(fragmentsParser, new LpsParser(commaSeparator | lineSeparator | spaceSeparator), whitespace));

            var scopedFragments = Lp.InBrackets(
                newLineParser.OptionalMultiple + start + newLineParser.OptionalMultiple,
                fragments.Maybe(),
                newLineParser.OptionalMultiple + end);

            var name = Lp.Name().Id(NameId) | _quotedTextParser.Parser.Wrap(NameId);

            var parserBody = (requirementParser.Parser + name + newLineParser.OptionalMultiple +
                             _annotationParser.Parser.Maybe()).Wrap(ChildStructureQueryHeaderId) + newLineParser.OptionalMultiple +
                             scopedFragments;

            Parser = new LpsParser(Id, true, parserBody + newLineParser.OptionalMultiple);

            structureQueryParser.Parser = parserBody;
        }

        public StructureFragment Parse(LpNode node)
        {
            return Parse(node, Id, false);
        }

        private StructureFragment Parse(LpNode node, string requiredId, bool restIsAllowed)
        {
            _nodeValidator.EnsureSuccess(node, requiredId, restIsAllowed);

            var headerNode = _nodeFinder.FindFirst(node, ChildStructureQueryHeaderId);

            var nameNode = _nodeFinder.FindFirst(headerNode, NameId);
            var quotedTextNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = quotedTextNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(quotedTextNode);

            var annotationMatch = _nodeFinder.FindFirst(headerNode, _annotationParser.Id);
            var annotation = annotationMatch != null ? _annotationParser.Parse(annotationMatch) : null;

            var fragmentNodes = _nodeFinder.FindAll(node, FragmentsId);

            var valueFragments = new List<ValueFragment>();
            var structureFragments = new List<StructureFragment>();

            foreach (var fragmentNode in fragmentNodes)
            {
                var child = fragmentNode.Children.Single();
                if (child.Id == _valueFragmentParser.Id)
                {
                    var valueQuery = _valueFragmentParser.Parse(child);
                    valueFragments.Add(valueQuery);
                }
                else if (child.Id == ChildStructureQueryId)
                {
                    var childStructureQuery = Parse(child, ChildStructureQueryId, true);
                    structureFragments.Add(childStructureQuery);
                }
            }

            var fragmentType =
                annotation == null ||
                annotation is SelectSingleNodeAnnotation ||
                annotation is SelectMultipleNodesAnnotation ||
                annotation is SelectCurrentNodeAnnotation
                ? FragmentType.Query
                : FragmentType.Mutation;

            if (valueFragments.Any(vf => vf.Type == FragmentType.Mutation))
            {
                fragmentType = FragmentType.Mutation;
            }

            return new StructureFragment(name, Plurality.Single, annotation, valueFragments.ToArray(), structureFragments.ToArray(), fragmentType);
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, ConstantSubject item, int itemIndex, SequencePart after)
        {
            // Make sure the operator can can actually be applied on the before/after SequencePart combination.
        }

        public bool CanValidate(ConstantSubject item)
        {
            return item is ObjectConstantSubject;
        }
    }
}
