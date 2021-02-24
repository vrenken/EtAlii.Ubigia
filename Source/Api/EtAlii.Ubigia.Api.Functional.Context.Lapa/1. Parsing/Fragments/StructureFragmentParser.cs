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
        //private readonly IRequirementParser _requirementParser
        private readonly INodeAnnotationsParser _annotationParser;

        private const string _nameId = "NameId";
        private const string _fragmentsId = "FragmentsId";
        private const string _childStructureQueryId = "ChildStructureQuery";
        private const string _childStructureQueryHeaderId = "ChildStructureQueryHeader";

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
            //_requirementParser = requirementParser

            var start = Lp.One(c => c == '{'); //.Debug("StartBracket")
            var end = Lp.One(c => c == '}'); //.Debug("EndBracket")

            var structureQueryParser = new LpsParser(_childStructureQueryId);

            var fragmentsParser = new LpsParser(_fragmentsId, true,
                structureQueryParser |
                _valueFragmentParser.Parser); //.Debug("VQ", true)

            var whitespace = whitespaceParser.Optional;//.Debug("W")
            var lineSeparator = whitespace + new LpsParser(Lp.Term("\r\n") | Lp.Term("\n")) + whitespace;

            //var spaceSeparator = whitespace + Lp.One(c => c == ' ' || c == '\t') + whitespace
            var spaceSeparator = whitespaceParser.Required;
            var commaSeparator = whitespace + new LpsParser((',' + whitespace + '\n') | ',') + whitespace;

            var fragments = new LpsParser(Lp.List(fragmentsParser, new LpsParser(commaSeparator | lineSeparator | spaceSeparator), whitespace));

            var scopedFragments = Lp.InBrackets(
                newLineParser.OptionalMultiple + start + newLineParser.OptionalMultiple,
                fragments.Maybe(),
                newLineParser.OptionalMultiple + end);

            var name = Lp.Name().Id(_nameId) | _quotedTextParser.Parser.Wrap(_nameId);

            var parserBody = (requirementParser.Parser + name + newLineParser.OptionalMultiple +
                             _annotationParser.Parser.Maybe()).Wrap(_childStructureQueryHeaderId) + newLineParser.OptionalMultiple +
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

            var headerNode = _nodeFinder.FindFirst(node, _childStructureQueryHeaderId);

            //var requirementNode = _nodeFinder.FindFirst(headerNode, _requirementParser.Id);
            //var requirement = requirementNode != null ? _requirementParser.Parse(requirementNode) : Requirement.None;

            var nameNode = _nodeFinder.FindFirst(headerNode, _nameId);
            var quotedTextNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = quotedTextNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(quotedTextNode);

            var annotationMatch = _nodeFinder.FindFirst(headerNode, _annotationParser.Id);
            var annotation = annotationMatch != null ? _annotationParser.Parse(annotationMatch) : null;

            var fragmentNodes = _nodeFinder.FindAll(node, _fragmentsId);

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
                else if (child.Id == _childStructureQueryId)
                {
                    var childStructureQuery = Parse(child, _childStructureQueryId, true);
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
