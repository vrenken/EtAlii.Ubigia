namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class FunctionSubjectParser : IFunctionSubjectParser
    {
        public string Id => _id;
        private readonly string _id = "FunctionSubject";

        public LpsParser Parser => _parser;
        private readonly LpsParser _parser;

        private readonly INodeValidator _nodeValidator;
        private readonly IFunctionSubjectArgumentsParser _functionSubjectArgumentsParser;
        private readonly INodeFinder _nodeFinder;

        private const string _nameId = "Name";
        private const string _parametersId = "Arguments";

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

            _parser = new LpsParser(Id, true,
                Lp.LetterOrDigit().OneOrMore().Id(_nameId) + 
                Lp.ZeroOrMore(' ') + 
                (
                    (
                        Lp.Char('(') +
                        firstParser.NextZeroOrMore(nextParser).Id(_parametersId) + 
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
            var text = _nodeFinder.FindFirst(node, _nameId).Match.ToString();

            var parameterNodes = _nodeFinder.FindFirst(node, _parametersId);

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
