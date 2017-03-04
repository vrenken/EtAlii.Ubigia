namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class ConditionalPathSubjectPartParser : IConditionalPathSubjectPartParser
    {
        public string Id => _id;
        private readonly string _id = "ConditionalPathSubjectPart";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IConditionParser _conditionParser;
        private readonly INewLineParser _newLineParser;
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
            _newLineParser = newLineParser;

            var separator = Lp.Char('&');//.Debug("Separator", true);
            _parser = new LpsParser(Id, true,
                Lp.One(c => c == '.') + //.Debug("Point") + 
                _newLineParser.OptionalMultiple + //.Debug("NL1") +  
                Lp.List(_conditionParser.Parser, separator, _newLineParser.OptionalMultiple));
                //Lp.List(_conditionParser.Parser.Debug("Kvp", true), separator, _newLineParser.OptionalMultiple.Debug("NL4")));
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
            if (partIndex == 0 || partIndex == 1 && (before is VariablePathSubjectPart) == false)
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
