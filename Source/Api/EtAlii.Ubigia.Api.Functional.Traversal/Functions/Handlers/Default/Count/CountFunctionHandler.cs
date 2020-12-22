namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class CountFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        private readonly ISelector<object, Func<IFunctionContext, ExecutionScope, object, Task<int>>> _converterSelector;

        public CountFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof(PathSubject))),
                new ParameterSet(false, new Parameter("var", typeof(Identifier))),
                new ParameterSet(false, new Parameter("var", typeof(IInternalNode))),
                new ParameterSet(false, new Parameter("var", typeof(IObservable<object>))),
            };
            Name = "Count";

            _converterSelector = new Selector<object, Func<IFunctionContext, ExecutionScope, object, Task<int>>>()
                .Register(o => o is PathSubject, (context, scope, o) => CountPath(context, (PathSubject)o, scope))
                .Register(o => o is Identifier, (_, _, _) => Task.FromResult(1))
                .Register(o => o is IInternalNode, (_, _, _) => Task.FromResult(1))
                .Register(o => o is IObservable<object>, (_, _, o) => CountObservable((IObservable<object>)o))
                .Register(o => o is IEnumerable<Identifier>, (_, _, o) => Task.FromResult(((IEnumerable<Identifier>)o).Count()))
                .Register(o => o is IEnumerable<IInternalNode>, (_, _, o) => Task.FromResult(((IEnumerable<IInternalNode>)o).Count()))
                .Register(o => o == null, (_, _, _) => Task.FromException<int>(new ScriptProcessingException("No empty argument is allowed for Count function processing")))
                .Register(_ => true, (_, _, _) => Task.FromException<int>(new ScriptProcessingException("Unable to convert arguments for Count function processing")));
        }

        public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            if (processAsSubject)
            {
                if (argumentSet.Arguments.Length == 0)
                {
                    output.OnCompleted();
                }
                else
                {
                    input = argumentSet.Arguments[0] as IObservable<object>;
                    if (input == null)
                    {
                        throw new ScriptProcessingException("Unable to convert arguments for Count function processing");
                    }
                }
            }
            else
            {
                if (argumentSet.Arguments.Length == 1)
                {
                    throw new ScriptProcessingException("Unable to use arguments and input data for Count function processing");
                }
            }
            Process(context, input, scope, output);
            return Task.CompletedTask;
        }

        private void Process(IFunctionContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            var result = 0;

            input.SubscribeAsync(
                onError: output.OnError,
                onCompleted: () =>
                {
                    output.OnNext(result);
                    output.OnCompleted();
                },
                onNext: async (o) =>
                {
                    var converter = _converterSelector.Select(o);
                    result += await converter(context, scope, o).ConfigureAwait(false);
                });
        }

        private async Task<int> CountPath(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(pathSubject, scope, outputObserver).ConfigureAwait(false);

                return Disposable.Empty;
            });

            var result = await outputObservable.Count();

            return result;
        }

        private async Task<int> CountObservable(IObservable<object> observable)
        {
            var result = await observable.Count();
            return result;
        }
    }
}
