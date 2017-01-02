namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class VariablePathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private const string Id = "VariablePathSubjectPart";
        private readonly ProcessingContext _context;
        private readonly PathSubjectParser _pathSubjectParser;
        private readonly Lazy<PathSubjectPartProcessor> _pathSubjectPartProcessor;
        private readonly LpsParser _parser;
        private readonly IParserHelper _parserHelper;
        private readonly ISelector<ScopeVariable, Func<ProcessParameters<PathSubjectPart, PathSubjectPart>, ScopeVariable, string, object>> _selector; 

        public VariablePathSubjectPartProcessor(
            ProcessingContext context,
            PathSubjectParser pathSubjectParser,
            Container container,
            IParserHelper parserHelper)
        {
            _context = context;
            _pathSubjectParser = pathSubjectParser;
            _pathSubjectPartProcessor = new Lazy<PathSubjectPartProcessor>(container.GetInstance<PathSubjectPartProcessor>);
            _parserHelper = parserHelper;

            _parser = new LpsParser(Id, true, _pathSubjectParser.Parser.OneOrMore());

            _selector = new Selector<ScopeVariable, Func<ProcessParameters<PathSubjectPart, PathSubjectPart>, ScopeVariable, string, object>>()
                .Register(variable => variable.Value is string, (parameters, variable, variableName) => ProcessAsString(parameters, (string) variable.Value, variableName))
                .Register(variable => variable.Value is INode, (parameters, variable, variableName) => ProcessAsNode(parameters, (INode)variable.Value, variableName))
                .Register(variable => variable.Value is PathSubject, (parameters, variable, variableName) => ProcessAsPathSubject(parameters, (PathSubject)variable.Value, variableName));
        }

        public object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            var variableName = ((VariablePathSubjectPart)parameters.Target).Name;
            ScopeVariable variable;
            if (!_context.Scope.Variables.TryGetValue(variableName, out variable))
            {
                string message = String.Format("Variable {0} not set (part: {0})", variableName, parameters.Target.ToString());
                throw new ScriptProcessingException(message);
            }

            if (variable == null)
            {
                string message = String.Format("Variable {0} not set (part: {0})", variableName, parameters.Target.ToString());
                throw new ScriptProcessingException(message);
            }

            var processor = _selector.Select(variable);
            var result = processor(parameters, variable, variableName);
            return result;
        }

        private object ProcessAsPathSubject(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters, PathSubject pathSubject, string variableName)
        {
            return ProcessPath(variableName, pathSubject, parameters);
        }

        private object ProcessAsString(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters, string value, string variableName)
        {
            var node = _parser.Do(value);
            _parserHelper.EnsureSuccess2(node, Id, false);
            var childNode = node.Children.Single();

            var pathSubject = ParsePath(value, variableName, childNode, parameters);
            return ProcessPath(variableName, pathSubject, parameters);
        }

        private object ProcessAsNode(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters, INode node, string variableName)
        {
            var pathSubject = new PathSubject(new IdentifierPathSubjectPart(node.Id));
            return ProcessPath(variableName, pathSubject, parameters);
        }

        private object ProcessPath(string variableName, PathSubject pathSubject, ProcessParameters<PathSubjectPart, PathSubjectPart> globalParameters)
        {
            object result = null;
            var parts = pathSubject.Parts;
            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                if (!_pathSubjectPartProcessor.Value.CanProcess(part))
                {
                    string message =
                        String.Format("Unable to process path part in variable (variable: {0}, path: {1}, part: {2})",
                            variableName, part.ToString(), globalParameters.Target.ToString());
                    throw new ScriptParserException(message);
                }

                var localParameters = new ProcessParameters<PathSubjectPart, PathSubjectPart>(part)
                {
                    LeftPart = i == 0 ? globalParameters.LeftPart : parts[i - 1],
                    LeftResult = i == 0 ? globalParameters.LeftResult : result,
                    RightPart = i == parts.Length - 1 ? globalParameters.RightPart : parts[i + 1],
                };
                result = _pathSubjectPartProcessor.Value.Process(localParameters);
            }
            return result;
        }

        private PathSubject ParsePath(string value, string variableName, LpNode childNode, ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            if (!_pathSubjectParser.CanParse(childNode))
            {
                string message = String.Format("Unable to parse variable for path (variable: {0}, value: {1}, part: {2})",
                    variableName, value, parameters.Target.ToString());
                throw new ScriptParserException(message);
            }
            var pathSubject = (PathSubject)_pathSubjectParser.Parse(childNode);

            if (!_pathSubjectParser.CanValidate(pathSubject))
            {
                string message = String.Format("Unable to validate path in variable (variable: {0}, path: {1}, part: {2})",
                    variableName, pathSubject.ToString(), parameters.Target.ToString());
                throw new ScriptParserException(message);
            }
            return pathSubject;
        }
    }
}
