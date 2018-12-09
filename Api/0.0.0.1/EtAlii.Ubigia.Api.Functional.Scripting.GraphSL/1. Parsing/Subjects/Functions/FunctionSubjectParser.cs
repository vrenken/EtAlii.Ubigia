namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class FunctionSubjectParser : IFunctionSubjectParser
    {
        public string Id { get; } = "FunctionSubject";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IFunctionSubjectArgumentsParser _functionSubjectArgumentsParser;
        private readonly INodeFinder _nodeFinder;

        private const string NameId = "Name";
        private const string ParametersId = "Arguments";

        public FunctionSubjectParser(
            INodeValidator nodeValidator,
            IFunctionSubjectArgumentsParser functionSubjectArgumentsParser,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;

            _functionSubjectArgumentsParser = functionSubjectArgumentsParser;
            _nodeFinder = nodeFinder;

            var firstParser = Lp.ZeroOrMore(' ') + _functionSubjectArgumentsParser.Parser + Lp.ZeroOrMore(' ');
            var nextParser = Lp.Char(',') + Lp.ZeroOrMore(' ') + _functionSubjectArgumentsParser.Parser + Lp.ZeroOrMore(' ');

            Parser = new LpsParser(Id, true,
                Lp.LetterOrDigit().OneOrMore().Id(NameId) + 
                Lp.ZeroOrMore(' ') + 
                (
                    (
                        Lp.Char('(') +
                        firstParser.NextZeroOrMore(nextParser).Id(ParametersId) + 
                        Lp.Char(')')
                    ) |
                    (
                        Lp.Char('(') +
                        Lp.ZeroOrMore(' ') + 
                        Lp.Char(')')
                    )
                ));
        }

        public Subject Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var text = _nodeFinder.FindFirst(node, NameId).Match.ToString();

            var parameterNodes = _nodeFinder.FindFirst(node, ParametersId);

            var childNodes = parameterNodes == null
                ? new LpNode[] { }
                : parameterNodes.Children
                    .Where(child => child.Id != null)
                    .ToArray();

            var parameters = childNodes.Select(childNode => _functionSubjectArgumentsParser.Parse(childNode)).ToArray();

            return new FunctionSubject(text, parameters);
        }


        public bool CanParse(LpNode node)
        {
            return node.Id == Id;
        }

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            var functionSubject = (FunctionSubject) subject;
            var parameters = functionSubject.Arguments;

            for (int i = 0; i < parameters.Length; i++)
            {
                var beforeParameter = i > 0 ? parameters[i - 1] : null;
                var afterParameter = i < parameters.Length - 1 ? parameters[i + 1] : null;
                var parameter = parameters[i];
                _functionSubjectArgumentsParser.Validate(beforeParameter, parameter, i, afterParameter);
            }


            functionSubject.ShouldAcceptInput = after != null;
        }

        public bool CanValidate(Subject subject)
        {
            return subject is FunctionSubject;
        }
    }
}
