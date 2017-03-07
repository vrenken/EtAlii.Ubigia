// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;
    using Moppet.Lapa;

    internal class VariablePathSubjectPartToGraphPathPartsConverter : IVariablePathSubjectPartToGraphPathPartsConverter
    {
        private const string _id = "VariablePathSubjectPart";
        private readonly IProcessingContext _context;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly LpsParser _nonRootedParser;
        private readonly INodeValidator _nodeValidator;
        private readonly ISelector<object, Func<object, string, Subject>> _converterSelector;
        private readonly ISelector<Subject,ISubjectParser> _parserSelector;

        public VariablePathSubjectPartToGraphPathPartsConverter(
            IProcessingContext context,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            INodeValidator nodeValidator, 
            IConstantSubjectsParser constantSubjectsParser)
        {
            _context = context;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _nonRootedParser = new LpsParser(_id, true, _nonRootedPathSubjectParser.Parser);

            _nodeValidator = nodeValidator;

            _converterSelector = new Selector<object, Func<object, string, Subject>>()
                .Register(variable => variable is string, (variable, variableName) => ToPathSubject((string)variable, variableName))
                .Register(variable => variable is INode, (variable, variableName) => ToPathSubject((INode)variable))
                .Register(variable => variable is PathSubject, (variable, variableName) => (Subject)variable);

            _parserSelector = new Selector<Subject, ISubjectParser>()
                .Register(s => s is NonRootedPathSubject, nonRootedPathSubjectParser)
                //.Register(s => s is RootedPathSubject, rootedPathSubjectParser)
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
                throw new ScriptProcessingException($"Variable {variableName} not set");
            }

            if (variable == null)
            {
                throw new ScriptProcessingException($"Variable {variableName} not assigned");
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
                result = pathSubject is RelativePathSubject
                    ? await ConvertAsStaticPath(pathSubject, scope)
                    : await ConvertAsDynamicPath(pathSubject, scope);
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
            var result = (await _context.PathSubjectToGraphPathConverter.Convert(pathSubject, scope))
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
                await _context.PathProcessor.Process(pathSubject, scope, outputObserver);

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
            // TODO: This class should also be able to cope with rooted paths.
            var node = _nonRootedParser.Do(value);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            return ParsePath(value, variableName, childNode);
        }

        private PathSubject ToPathSubject(INode node)
        {
            return new RelativePathSubject(new IdentifierPathSubjectPart(node.Id));
        }

        private Subject ParsePath(string value, string variableName, LpNode childNode)
        {
            if (!_nonRootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse variable for path (variable: {variableName}, value: {value})");
            }
            var pathSubject = _nonRootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we recieve a string constant that needs to be validated validation.
            var parser = _parserSelector.Select(pathSubject);
            if (!parser.CanValidate(pathSubject))
            {
                throw new ScriptParserException($"Unable to validate path in variable (variable: {variableName}, path: {pathSubject})");
            }
            return pathSubject;
        }
    }
}