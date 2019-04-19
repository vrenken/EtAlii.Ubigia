namespace EtAlii.Ubigia.Api.Functional
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

        private readonly ISelector<object, Func<IFunctionContext, ExecutionScope, object, int>> _converterSelector;

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

            _converterSelector = new Selector<object, Func<IFunctionContext, ExecutionScope, object, int>>()
                .Register(o => o is PathSubject, (context, scope, o) => CountPath(context, (PathSubject)o, scope))
                .Register(o => o is Identifier, (context, scope, o) => 1)
                .Register(o => o is IInternalNode, (context, scope, o) => 1)
                .Register(o => o is IObservable<object>, (context, scope, o) => CountObservable((IObservable<object>)o))
                .Register(o => o is IEnumerable<Identifier>, (context, scope, o) => ((IEnumerable<Identifier>)o).Count())
                .Register(o => o is IEnumerable<IInternalNode>, (context, scope, o) => ((IEnumerable<IInternalNode>)o).Count())
                .Register(o => o == null, (context, scope, o) => { throw new ScriptProcessingException("No empty argument is allowed for Count function processing");})
                .Register(o => true, (context, scope, o) => { throw new ScriptProcessingException("Unable to convert arguments for Count function processing");});
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
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
        }

        private void Process(IFunctionContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            int result = 0;

            input.Subscribe(
                onError: output.OnError,
                onCompleted: () =>
                {
                    output.OnNext(result);
                    output.OnCompleted();
                },
                onNext: (o) =>
                {
                    var converter = _converterSelector.Select(o);
                    result += converter(context, scope, o);
                });
        }

        private int CountPath(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(pathSubject, scope, outputObserver);

                return Disposable.Empty;
            });

            int result = 0;

            var task = Task.Run(async () =>
            {
                result = await outputObservable.Count();
            });
            task.Wait();

            return result;
        }

        private int CountObservable(IObservable<object> observable)
        {

            int result = 0;

            var task = Task.Run(async () =>
            {
                result = await observable.Count();
            });
            task.Wait();

            return result;
        }
    }
}