// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class VariablePathSubjectPartToGraphPathPartsConverter : IVariablePathSubjectPartToGraphPathPartsConverter
    {
        private const string Id = "VariablePathSubjectPart";
        private readonly IProcessingContext _context;
        private readonly IPathSubjectParser _pathSubjectParser;
        private readonly Lazy<IPathProcessor> _pathProcessor;
        private readonly Lazy<IPathSubjectToGraphPathConverter> _pathSubjectToGraphPathConverter;
        private readonly LpsParser _parser;
        private readonly INodeValidator _nodeValidator;
        private readonly ISelector<object, Func<object, string, Subject>> _converterSelector;
        private readonly ISelector<Subject,ISubjectParser> _parserSelector;

        public VariablePathSubjectPartToGraphPathPartsConverter(
            IProcessingContext context,
            IPathSubjectParser pathSubjectParser,
            INodeValidator nodeValidator, 
            IConstantSubjectsParser constantSubjectsParser)
        {
            _context = context;
            _pathSubjectParser = pathSubjectParser;
            _pathProcessor = new Lazy<IPathProcessor>(() => context.PathProcessor);
            _pathSubjectToGraphPathConverter = new Lazy<IPathSubjectToGraphPathConverter>(() => context.PathSubjectToGraphPathConverter);
            _nodeValidator = nodeValidator;
            
            _parser = new LpsParser(Id, true, _pathSubjectParser.Parser.OneOrMore());

            _converterSelector = new Selector<object, Func<object, string, Subject>>()
                .Register(variable => variable is string, (variable, variableName) => ToPathSubject((string)variable, variableName))
                .Register(variable => variable is INode, (variable, variableName) => ToPathSubject((INode)variable))
                .Register(variable => variable is PathSubject, (variable, variableName) => (Subject)variable);

            _parserSelector = new Selector<Subject, ISubjectParser>()
                .Register(s => s is PathSubject, pathSubjectParser)
                .Register(s => s is ConstantSubject, constantSubjectsParser);
        }

        public async Task<GraphPathPart[]> Convert(
            PathSubjectPart pathSubjectPart, 
            int pathSubjectPartPosition, 
            PathSubjectPart previousPathSubjectPart, 
            PathSubjectPart nextPathSubjectPart, 
            ExecutionScope scope)
        {
            var variableName = ((VariablePathSubjectPart)pathSubjectPart).Name;
            ScopeVariable variable;
            if (!_context.Scope.Variables.TryGetValue(variableName, out variable))
            {
                var message = String.Format("Variable {0} not set", variableName);
                throw new ScriptProcessingException(message);
            }

            if (variable == null)
            {
                var message = String.Format("Variable {0} not assigned", variableName);
                throw new ScriptProcessingException(message);
            }

            // TODO: At this moment we only allow single items to be used as path variables.
            var variableValue = await variable.Value.SingleAsync();
            var converter = _converterSelector.Select(variableValue);
            var subject = converter(variableValue, variableName);


            // We should be able to cope with string constants as well.
            GraphPath result;
            var pathSubject = subject as PathSubject;
            var stringConstantSubject = subject as StringConstantSubject;
            if (pathSubject != null)
            {
                result = pathSubject.IsAbsolute
                    ? await ConvertAsDynamicPath(pathSubject, scope)
                    : await ConvertAsStaticPath(pathSubject, scope);
            }
            else if(stringConstantSubject != null)
            {
                var graphNode = new GraphNode(stringConstantSubject.Value);
                result = new GraphPath(graphNode);
            }
            else
            {
                throw new ScriptParserException("No valid subject found on which a GraphPath can be build");
            }
            return result.ToArray();
        }

        private async Task<GraphPath> ConvertAsStaticPath(PathSubject pathSubject, ExecutionScope scope)
        {
            var result = (await _pathSubjectToGraphPathConverter.Value.Convert(pathSubject, scope))
                .ToArray();

            // A static path cannot have GraphRootStartNodes.
            for (int i = 0; i < result.Length; i++)
            {
                var graphRootStartNode = result[i] as GraphRootStartNode;
                if (graphRootStartNode != null)
                {
                    var root = graphRootStartNode.Root;
                    result[i] = new GraphNode(root);
                }
            }
            return new GraphPath(result);
        }

        private async Task<GraphPath> ConvertAsDynamicPath(PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await _pathProcessor.Value.Process(pathSubject, scope, outputObserver);

                return Disposable.Empty;
            });

            var entries = (await outputObservable.ToArray())
                .Cast<IReadOnlyEntry>()
                .ToArray();

            var startIdentifiers = entries
                .Select(e => e.Id)
                .ToArray();
            return GraphPath.Create(startIdentifiers);
        }

        private Subject ToPathSubject(string value, string variableName)
        {
            var node = _parser.Do(value);
            _nodeValidator.EnsureSuccess(node, Id, false);
            var childNode = node.Children.Single();

            return ParsePath(value, variableName, childNode);
        }

        private PathSubject ToPathSubject(INode node)
        {
            return new PathSubject(new IdentifierPathSubjectPart(node.Id));
        }

        private Subject ParsePath(string value, string variableName, LpNode childNode)
        {
            if (!_pathSubjectParser.CanParse(childNode))
            {
                string message = String.Format("Unable to parse variable for path (variable: {0}, value: {1})", variableName, value);
                throw new ScriptParserException(message);
            }
            var pathSubject = _pathSubjectParser.Parse(childNode);

            // There is a possibility that we recieve a string constant that needs to be validated validation.
            var parser = _parserSelector.Select(pathSubject);
            if (!parser.CanValidate(pathSubject))
            {
                string message = String.Format("Unable to validate path in variable (variable: {0}, path: {1})", variableName, pathSubject.ToString());
                throw new ScriptParserException(message);
            }
            return pathSubject;
        }
    }
}