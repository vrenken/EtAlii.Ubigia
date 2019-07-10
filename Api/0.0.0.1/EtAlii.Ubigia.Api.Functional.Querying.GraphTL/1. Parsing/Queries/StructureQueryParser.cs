namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using Moppet.Lapa;

    internal class StructureQueryParser : IStructureQueryParser
    {
        public string Id { get; } = nameof(StructureQuery);

        private const string ChildStructureQueryId = "ChildStructureQuery"; 
        private const string ChildStructureQueryHeaderId = "ChildStructureQueryHeader"; 
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

            var structureQueryParser = new LpsParser(ChildStructureQueryId);
            //structureQueryParser.Parser =  
            var fragmentParsers =
            (
                structureQueryParser |
                //_valueMutationParser.Parser | //.Debug("VM", true) | 
                _valueQueryParser.Parser //.Debug("VQ", true)
            );
            var fragmentsParser = new LpsParser(FragmentsId, true, fragmentParsers);

//            var fragments  = new LpsParser(new[]
//            {
//                Lp.List(fragmentsParser, newLineParser.Required, whitespace),
//                Lp.List(fragmentsParser, separator, newLineParser.OptionalMultiple),
//            }.Aggregate(new LpsAlternatives(), (current, parser) => current | parser));
            
            var whitespace = Lp.ZeroOrMore(c => c == ' ' || c == '\t' || c == '\r');//.Debug("W");
            var lineSeparator = whitespace + new LpsParser(Lp.Term("\r\n") | Lp.Term("\n")) + whitespace; 
            var spaceSeparator = whitespace + Lp.One(c => c == ' ' || c == '\t') + whitespace; 
            var commaSeparator = whitespace + new LpsParser((',' + whitespace + '\n') | ',') + whitespace; 

            var fragments = new LpsParser(Lp.List(fragmentsParser, new LpsParser(commaSeparator | lineSeparator | spaceSeparator), whitespace));

//            var fragments = new LpsParser(
//                    Lp.List(fragmentsParser, commaSeparator, newLineParser.OptionalMultiple ).Debug("L1", true) | 
//                    Lp.List(fragmentsParser, lineSeparator, whitespace ).Debug("L2", true) |
//                    Lp.List(fragmentsParser, spaceSeparator, newLineParser.OptionalMultiple).Debug("L3", true) 
//                    );
            
            var scopedFragments = Lp.InBrackets(
                newLineParser.OptionalMultiple + start + newLineParser.OptionalMultiple,
                fragments.Maybe(),
                newLineParser.OptionalMultiple + end);

            var name = (Lp.Name().Id(NameId) | _quotedTextParser.Parser.Wrap(NameId));

            var parserBody = (_requirementParser.Parser + name + newLineParser.OptionalMultiple +
                             _annotationParser.Parser.Maybe()).Wrap(ChildStructureQueryHeaderId) + newLineParser.OptionalMultiple +
                             scopedFragments;

            Parser = new LpsParser(Id, true, parserBody + newLineParser.OptionalMultiple);

            structureQueryParser.Parser = parserBody;//.Copy();
        }

        public StructureQuery Parse(LpNode node)
        {
            return Parse(node, Id, false);
        }

        private StructureQuery Parse(LpNode node, string requiredId, bool restIsAllowed)
        {
            _nodeValidator.EnsureSuccess(node, requiredId, restIsAllowed);

            var headerNode = _nodeFinder.FindFirst(node, ChildStructureQueryHeaderId);
            
            var requirementNode = _nodeFinder.FindFirst(headerNode, _requirementParser.Id);
            var requirement = requirementNode != null ? _requirementParser.Parse(requirementNode) : Requirement.None;

            var nameNode = _nodeFinder.FindFirst(headerNode, NameId);
            var quotedTextNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = quotedTextNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(quotedTextNode);
            
            var annotationMatch = _nodeFinder.FindFirst(headerNode, _annotationParser.Id);
            var annotation = annotationMatch != null ? _annotationParser.Parse(annotationMatch) : null;

            var fragmentNodes = _nodeFinder.FindAll(node, FragmentsId);

            var values = new List<ValueQuery>();
            var children = new List<StructureQuery>();
            
            foreach (var fragmentNode in fragmentNodes)
            {
                var child = fragmentNode.Children.Single(); 
                if (child.Id == _valueQueryParser.Id)
                {
                    var valueQuery = _valueQueryParser.Parse(child);
                    values.Add(valueQuery);
                }
                else if (child.Id == ChildStructureQueryId)
                {
                    var childStructureQuery = Parse(child, ChildStructureQueryId, true);
                    children.Add(childStructureQuery);
                }
            }
            
            return new StructureQuery(name, annotation, requirement, values.ToArray(), children.ToArray());
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
