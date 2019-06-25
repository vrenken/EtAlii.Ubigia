namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using Moppet.Lapa;

    internal class StructureMutationParser : IStructureMutationParser
    {
        public string Id { get; } = nameof(StructureMutation);

        private const string ChildStructureMutationId = "ChildStructureMutation"; 
        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly IQuotedTextParser _quotedTextParser;
        private readonly IValueMutationParser _valueMutationParser;
        private readonly IValueQueryParser _valueQueryParser;

        private readonly IAnnotationParser _annotationParser;
        private const string _nameId = "NameId";
        private const string FragmentsId = "FragmentsId";

        public StructureMutationParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            IQuotedTextParser quotedTextParser,
            IValueQueryParser valueQueryParser,
            IAnnotationParser annotationParser, 
            IValueMutationParser valueMutationParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _quotedTextParser = quotedTextParser;
            _valueQueryParser = valueQueryParser;
            _annotationParser = annotationParser;
            _valueMutationParser = valueMutationParser;

            var start = Lp.One(c => c == '{'); //.Debug("StartBracket")
            var end = Lp.One(c => c == '}'); //.Debug("EndBracket")

            var structureMutationParser = new LpsParser(ChildStructureMutationId);
            var fragmentParsers =
            (
                structureMutationParser |
                _valueMutationParser.Parser | //.Debug("VM", true) | 
                _valueQueryParser.Parser //.Debug("VQ", true)
            );
            var fragmentsParser = new LpsParser(FragmentsId, true, fragmentParsers);
            
                        
            var whitespace = Lp.ZeroOrMore(c => c == ' ' || c == '\t' || c == '\r');
            var lineSeparator = whitespace + Lp.One(c => c == '\n') + whitespace; 
            var spaceSeparator = whitespace + Lp.One(c => c == ' ' || c == '\t') + whitespace; 
            var commaSeparator = whitespace + ((',' + whitespace + '\n') | ',') + whitespace; 

            var fragments = new LpsParser(Lp.List(fragmentsParser, new LpsParser(commaSeparator | lineSeparator | spaceSeparator), whitespace));
            
            var scopedFragments = Lp.InBrackets(
                newLineParser.OptionalMultiple + start + newLineParser.OptionalMultiple,
                fragments.Maybe(),
                newLineParser.OptionalMultiple + end + newLineParser.OptionalMultiple);

            var name = ( Lp.Name().Id(_nameId) | _quotedTextParser.Parser.Wrap(_nameId) );

            var parserBody = name + newLineParser.OptionalMultiple + 
                             _annotationParser.Parser.Maybe() + newLineParser.OptionalMultiple +
                             scopedFragments;

            Parser = new LpsParser(Id, true, parserBody);

            structureMutationParser.Parser = parserBody;//.Copy();
        }

        public StructureMutation Parse(LpNode node)
        {
            return Parse(node, Id, false);
        }

        private StructureMutation Parse(LpNode node, string requiredId, bool restIsAllowed)
        {
            _nodeValidator.EnsureSuccess(node, requiredId, restIsAllowed);

            var nameNode = _nodeFinder.FindFirst(node, _nameId);
            var quotedTextNode = nameNode.FirstOrDefault(n => n.Id == _quotedTextParser.Id);
            var name = quotedTextNode == null ? nameNode.Match.ToString() : _quotedTextParser.Parse(quotedTextNode);
            
            var annotationMatch = _nodeFinder.FindFirst(node, _annotationParser.Id);
            var annotation = annotationMatch != null ? _annotationParser.Parse(annotationMatch) : null;

            var fragmentNodes = _nodeFinder.FindAll(node, FragmentsId);

            var fragments = new List<Fragment>();
            foreach (var fragmentNode in fragmentNodes)
            {
                var child = fragmentNode.Children.Single(); 
                if (child.Id == _valueMutationParser.Id)
                {
                    var valueMutation = _valueMutationParser.Parse(child);
                    fragments.Add(valueMutation);
                }
                else if (child.Id == _valueQueryParser.Id)
                {
                    var valueQuery = _valueQueryParser.Parse(child);
                    fragments.Add(valueQuery);
                }
                else if (child.Id == ChildStructureMutationId)
                {
                    var childStructureQuery = Parse(child, ChildStructureMutationId, true);
                    fragments.Add(childStructureQuery);
                }
            }
            
            return new StructureMutation(name, annotation, fragments.ToArray());
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
