namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using Moppet.Lapa;

    internal class StructureQueryParser : IStructureQueryParser
    {
        public string Id { get; } = nameof(StructureQuery);

        private const string ChildStructureQueryId = "ChildStructureQuery"; 
        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IValueMutationParser _valueMutationParser;
        private readonly IValueQueryParser _valueQueryParser;
        private readonly IRequirementParser _requirementParser;

        private readonly IAnnotationParser _annotationParser;
        private const string NameId = "NameId";
        private const string FragmentsId = "FragmentsId";

        public StructureQueryParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            IQuotedTextParser quotedTextParser,
            IValueQueryParser valueQueryParser,
            IAnnotationParser annotationParser, 
            IValueMutationParser valueMutationParser, 
            IRequirementParser requirementParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _quotedTextParser = quotedTextParser;
            _valueQueryParser = valueQueryParser;
            _annotationParser = annotationParser;
            _valueMutationParser = valueMutationParser;
            _requirementParser = requirementParser;

            var start = Lp.One(c => c == '{'); //.Debug("StartBracket")
            var end = Lp.One(c => c == '}'); //.Debug("EndBracket")

            var separator = Lp.Char(',');

            var structureQueryParser = new LpsParser(ChildStructureQueryId);
            //structureQueryParser.Parser =  
            var fragmentParsers =
            (
                structureQueryParser |
                _valueMutationParser.Parser | //.Debug("VM", true) | 
                _valueQueryParser.Parser //.Debug("VQ", true)
            );
            var fragmentsParser = new LpsParser(FragmentsId, true, fragmentParsers);
            
            var fragments = new LpsParser(
                Lp.List(fragmentsParser, separator, newLineParser.OptionalMultiple) |
                Lp.List(fragmentsParser, newLineParser.Required, newLineParser.OptionalMultiple));
            
            var scopedFragments = Lp.InBrackets(
                newLineParser.OptionalMultiple + start + newLineParser.OptionalMultiple,
                fragments.Maybe(),
                newLineParser.OptionalMultiple + end + newLineParser.OptionalMultiple);

            var name = (Lp.Name().Id(NameId) | _quotedTextParser.Parser.Wrap(NameId));

            var parserBody = _requirementParser.Parser + name + newLineParser.OptionalMultiple +
                             _annotationParser.Parser.Maybe() + newLineParser.OptionalMultiple +
                             scopedFragments;

            Parser = new LpsParser(Id, true, parserBody);

            structureQueryParser.Parser = parserBody;//.Copy();
        }

        public StructureQuery Parse(LpNode node)
        {
            return Parse(node, Id, false);
        }

        private StructureQuery Parse(LpNode node, string requiredId, bool restIsAllowed)
        {
            _nodeValidator.EnsureSuccess(node, requiredId, restIsAllowed);

            var requirementNode = _nodeFinder.FindFirst(node, _requirementParser.Id);
            var requirement = requirementNode != null ? _requirementParser.Parse(requirementNode) : Requirement.None;

            var nameNode = _nodeFinder.FindFirst(node, NameId);
            var quotedTextNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = quotedTextNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(quotedTextNode);
            
            var annotationMatch = _nodeFinder.FindFirst(node, _annotationParser.Id);
            var annotation = annotationMatch != null ? _annotationParser.Parse(annotationMatch) : null;

            var fragmentNodes = _nodeFinder.FindAll(node, FragmentsId);

            var fragments = new List<QueryFragment>();
            foreach (var fragmentNode in fragmentNodes)
            {
                var child = fragmentNode.Children.Single(); 
                if (child.Id == _valueQueryParser.Id)
                {
                    var valueQuery = _valueQueryParser.Parse(child);
                    fragments.Add(valueQuery);
                }
                else if (child.Id == ChildStructureQueryId)
                {
                    var childStructureQuery = Parse(child, ChildStructureQueryId, true);
                    fragments.Add(childStructureQuery);
                }
            }
            
            return new StructureQuery(name, annotation, requirement, fragments.ToArray());
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, ConstantSubject subject, int constantSubjectIndex, SequencePart after)
        {
            // Make sure the operator can can actually be applied on the before/after SequencePart combination.
        }

        public bool CanValidate(ConstantSubject constantSubject)
        {
            return constantSubject is ObjectConstantSubject;
        }
    }
}
