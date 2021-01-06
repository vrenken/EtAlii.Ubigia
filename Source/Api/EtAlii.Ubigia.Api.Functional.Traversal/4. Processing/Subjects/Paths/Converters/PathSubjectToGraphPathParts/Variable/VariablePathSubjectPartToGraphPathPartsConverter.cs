// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class VariablePathSubjectPartToGraphPathPartsConverter : IVariablePathSubjectPartToGraphPathPartsConverter
    {
        private readonly IScriptProcessingContext _context;
        private readonly ISelector<object, Func<object, string, Subject>> _converterSelector;
        private readonly IScriptParser _scriptParser;

        public VariablePathSubjectPartToGraphPathPartsConverter(
            IScriptProcessingContext context,
            IScriptParser scriptParser)
        {
            _context = context;
            _scriptParser = scriptParser;

            _converterSelector = new Selector<object, Func<object, string, Subject>>()
                .Register(variable => variable is string, (variable, variableName) => ToPathSubject((string)variable, variableName))
                .Register(variable => variable is INode, (variable, _) => new RelativePathSubject(new IdentifierPathSubjectPart(((INode)variable).Id)))
                .Register(variable => variable is PathSubject, (variable, _) => (Subject)variable);
        }

        public async Task<GraphPathPart[]> Convert(
            PathSubjectPart pathSubjectPart,
            int pathSubjectPartPosition,
            PathSubjectPart previousPathSubjectPart,
            PathSubjectPart nextPathSubjectPart,
            ExecutionScope scope)
        {
            var variableName = ((VariablePathSubjectPart)pathSubjectPart).Name;
            if (!_context.Scope.Variables.TryGetValue(variableName, out var variable))
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
            if (subject is PathSubject pathSubject)
            {
                result = pathSubject is RelativePathSubject
                    ? await ConvertAsStaticPath(pathSubject, scope).ConfigureAwait(false)
                    : await ConvertAsDynamicPath(pathSubject, scope).ConfigureAwait(false);
            }
            else if(subject is StringConstantSubject stringConstantSubject)
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
            var path = await _context.PathSubjectToGraphPathConverter.Convert(pathSubject, scope).ConfigureAwait(false);
            var result = path.ToArray();

            // A static path cannot have GraphRootStartNodes.
            for (var i = 0; i < result.Length; i++)
            {
                if (result[i] is GraphRootStartNode graphRootStartNode)
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
                await _context.PathProcessor.Process(pathSubject, scope, outputObserver).ConfigureAwait(false);

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
            try
            {
                return _scriptParser.ParsePath(value);
            }
            catch (Exception e)
            {
                throw new ScriptParserException($"Unable to parse variable for path (variable: {variableName}, value: {value})", e);
            }
        }
    }
}
