namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class FunctionSubjectParser : IFunctionSubjectParser
    {
        public string Id { get { return _id; } }
        private readonly string _id = "FunctionSubject";

        public LpsParser Parser { get { return _parser; } }
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IFunctionSubjectArgumentsParser _functionSubjectArgumentsParser;
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly INodeFinder _nodeFinder;

        private const string NameId = "Name";
        private const string ParametersId = "Arguments";

        public FunctionSubjectParser(
            INodeValidator nodeValidator,
            IFunctionSubjectArgumentsParser functionSubjectArgumentsParser,
            IFunctionHandlersProvider functionHandlersProvider,
            INodeFinder nodeFinder)
        {
            _nodeValidator = nodeValidator;

            _functionSubjectArgumentsParser = functionSubjectArgumentsParser;
            _functionHandlersProvider = functionHandlersProvider;
            _nodeFinder = nodeFinder;

            var firstParser = Lp.ZeroOrMore(' ') + _functionSubjectArgumentsParser.Parser + Lp.ZeroOrMore(' ');
            var nextParser = Lp.Char(',') + Lp.ZeroOrMore(' ') + _functionSubjectArgumentsParser.Parser + Lp.ZeroOrMore(' ');

            _parser = new LpsParser(Id, true,
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

            // Find function handler.
            var functionHandler = FindFunctionHandler(functionSubject);
            // Find ParameterSets with the same length.
            var parameterSets = FindParameterSetsWithMatchingLength(functionSubject, functionHandler, after != null);

            functionSubject.FunctionHandler = functionHandler;
            functionSubject.PossibleParameterSets = parameterSets;
        }

        private ParameterSet[] FindParameterSetsWithMatchingLength(FunctionSubject functionSubject, IFunctionHandler functionHandler, bool shouldAcceptInput)
        {
            var parameterCount = functionSubject.Arguments.Length;
            var matchingParameterSets =
                functionHandler.ParameterSets
                .Where(parameterSet => parameterSet.Parameters.Length == parameterCount)
                .ToArray();
            if (!matchingParameterSets.Any())
            {
                var message = String.Format("No function '{0}' found with {1} parameters", functionSubject.Name, parameterCount);
                throw new ScriptParserException(message);
            }

            matchingParameterSets = matchingParameterSets
                .Where(args => args.RequiresInput == shouldAcceptInput)
                .ToArray();
            if (!matchingParameterSets.Any())
            {
                var message = String.Format("No function '{0}' found with {1} parameters that also accepts input", functionSubject.Name, parameterCount);
                throw new ScriptParserException(message);
            }

            return matchingParameterSets;
        }

        private IFunctionHandler FindFunctionHandler(FunctionSubject functionSubject)
        {
            var functionHandler = _functionHandlersProvider.FunctionHandlers.SingleOrDefault(fhc => String.Equals(fhc.Name, functionSubject.Name, StringComparison.OrdinalIgnoreCase));
            if (functionHandler == null)
            {
                var message = String.Format("No function found with name '{0}'", functionSubject.Name);
                throw new ScriptParserException(message);
            }
            return functionHandler;
        }

        public bool CanValidate(Subject subject)
        {
            return subject is FunctionSubject;
        }
    }
}
