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
        private readonly IValueMutationParser _valueMutationParser;
        private readonly IValueQueryParser _valueQueryParser;

        private readonly IAnnotationParser _annotationParser;
        private const string _nameId = "NameId";
        private const string FragmentsId = "FragmentsId";

        public StructureQueryParser(
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            IValueQueryParser valueQueryParser,
            IAnnotationParser annotationParser, 
            IValueMutationParser valueMutationParser)
        {
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _valueQueryParser = valueQueryParser;
            _annotationParser = annotationParser;
            _valueMutationParser = valueMutationParser;

            var start = Lp.One(c => c == '{'); //.Debug("StartBracket")
            var end = Lp.One(c => c == '}'); //.Debug("EndBracket")

            var newlinedWhiteSpace = Lp.ZeroOrMore(c => c == ' ' || c == '\t' || c == '\n' || c == '\r');//.Debug("Whitespace", true);

            
            var separator = Lp.Char(',');

            var structureQueryParser = new LpsParser(ChildStructureQueryId);
            //structureQueryParser.Parser =  
            var fragmentParsers =
            (
                _valueMutationParser.Parser | //.Debug("VM", true) | 
                _valueQueryParser.Parser | //.Debug("VQ", true)
                structureQueryParser
            );
            var fragmentsParser = new LpsParser(FragmentsId, true, fragmentParsers);
            
            var fragments = Lp.List(fragmentsParser, separator, newlinedWhiteSpace);
            var scopedFragments = Lp.InBrackets(
                newlinedWhiteSpace + start + newlinedWhiteSpace,
                fragments,
                newlinedWhiteSpace + end + newlinedWhiteSpace);

            var parserBody = Lp.Name().Id(_nameId) + newlinedWhiteSpace +
                             _annotationParser.Parser.Maybe() + newlinedWhiteSpace +
                             scopedFragments;

            Parser = new LpsParser(Id, true, parserBody);

            structureQueryParser.Parser = parserBody;
        }

        public StructureQuery Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);

            var nameNode = _nodeFinder.FindFirst(node, _nameId);
            var name = nameNode.Match.ToString();
            
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
                else if (child.Id == ChildStructureQueryId)
                {
                    var childStructureQuery = Parse(child);
                    fragments.Add(childStructureQuery);
                }
            }
            
            return new StructureQuery(name, annotation, fragments.ToArray());
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
