namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ConditionalPathSubjectPartParser : IConditionalPathSubjectPartParser
    {
        public string Id { get; } = nameof(ConditionalPathSubjectPart);

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IConditionParser _conditionParser;
        private readonly INodeFinder _nodeFinder;

        public ConditionalPathSubjectPartParser(
            INodeValidator nodeValidator,
            IConditionParser conditionParser,
            INewLineParser newLineParser,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;
            _conditionParser = conditionParser;
            _nodeFinder = nodeFinder;

            var separator = Lp.Char('&');
            Parser = new LpsParser(Id, true,
                Lp.One(c => c == '.') +  
                newLineParser.OptionalMultiple + 
                Lp.List(_conditionParser.Parser, separator, newLineParser.OptionalMultiple));
        }

        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            
            var conditions = _nodeFinder
                .FindAll(node, _conditionParser.Id)
                .Select(n => _conditionParser.Parse(n))
                .ToArray();

            return new ConditionalPathSubjectPart(conditions);
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            if (partIndex == 0 || partIndex == 1 && !(before is VariablePathSubjectPart))
            {
                throw new ScriptParserException("A conditional path part cannot be used at the beginning of a graph path.");
            }
        }

        public bool CanValidate(PathSubjectPart part)
        {
            return part is ConditionalPathSubjectPart;
        }
    }
}
