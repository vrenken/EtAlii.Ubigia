namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class RenameFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        private readonly ISelector<object, Func<IFunctionContext, object, ExecutionScope, IObservable<Identifier>>> _toIdentifierConverterSelector;
        private readonly ISelector<int, object, object, Func<object, object, string>> _toNameConverterSelector;

        public RenameFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true, new Parameter("name", typeof(string))),
                new ParameterSet(true, new Parameter("name", typeof(IObservable<object>))),
                new ParameterSet(false, new Parameter("var", typeof(string)), new Parameter("name", typeof(string))),
                new ParameterSet(false, new Parameter("var", typeof(PathSubject)), new Parameter("name", typeof(string))),
                new ParameterSet(false, new Parameter("var", typeof(IObservable<object>)), new Parameter("name", typeof(string))),
                new ParameterSet(false, new Parameter("var", typeof(string)), new Parameter("name", typeof(IObservable<object>))),
                new ParameterSet(false, new Parameter("var", typeof(PathSubject)), new Parameter("name", typeof(IObservable<object>))),
                new ParameterSet(false, new Parameter("var", typeof(IObservable<object>)), new Parameter("name", typeof(IObservable<object>)))
            };
            Name = "Rename";

            _toIdentifierConverterSelector = new Selector<object, Func<IFunctionContext, object, ExecutionScope, IObservable<Identifier>>>()
                .Register(i => i is PathSubject, (c, i, s) => ConvertPathToIds(c, (PathSubject)i, s))
                .Register(i => i is Identifier, (c, i, s) => Observable.Return((Identifier)i))
                .Register(i => i is IInternalNode, (c, i, s) => Observable.Return(((IInternalNode)i).Id))
                .Register(i => true, (c, i, s) => { throw new ScriptProcessingException("Unable to convert input for Rename function processing"); });

            _toNameConverterSelector = new Selector2<int, object, object, Func<object, object, string>>()
                .Register((c, f, s) => c == 1 && f is string, (f, s) => (string)f)
                .Register((c, f, s) => c == 1 && f is IObservable<object>, (f, s) => ((IObservable<object>)f).ToEnumerable().Cast<string>().Single())
                .Register((c, f, s) => c == 2 && s is string, (f, s) => (string)s)
                .Register((c, f, s) => c == 2 && s is IObservable<object>, (f, s) => ((IObservable<object>)s).ToEnumerable().Cast<string>().Single())
                .Register((c, f, s) => true, (f, s) => { throw new ScriptProcessingException("Unable to convert name input for Rename function processing"); });
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            if (processAsSubject)
            {
                if (argumentSet.Arguments.Length == 2)
                {
                    ProcessByArgument(context, parameterSet, argumentSet, scope, output);
                }
                else
                {
                    // No way to throw an exception here. It could be a left side subject so we will have to wait until it is executed from an operator.
                    //throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing");
                    output.OnCompleted();
                }
            }
            else
            {
                if (argumentSet.Arguments.Length == 1)
                {
                    ProcessByInput(context, parameterSet, argumentSet, input, scope, output);
                }
                else
                {
                    throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing");
                }
            }
        }

        private void ProcessByArgument(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, ExecutionScope scope, IObserver<object> output)
        {
            var c = argumentSet.Arguments.Length;
            var f = argumentSet.Arguments.Length > 0 ? argumentSet.Arguments[0] : null;
            var s = argumentSet.Arguments.Length > 1 ? argumentSet.Arguments[1] : null;
            var nameConverter = _toNameConverterSelector.Select(c, f, s);
            var newName = nameConverter(f, s);

            var input = argumentSet.Arguments[0] as IObservable<object>;
            if (input == null)
            {
                throw new ScriptProcessingException("Unable to convert arguments for Rename function processing");
            }
            input.Subscribe(
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: o =>
                {
                    var task = Task.Run(async () =>
                    {
                        var converter = _toIdentifierConverterSelector.Select(o);
                        var results = converter(context, o, scope);
                        foreach (var result in results.ToEnumerable())
                        {

                            var renamedItem = await context.PathProcessor.Context.Logical.Nodes.Rename(result, newName, scope);
                            output.OnNext(renamedItem);
                        }
                    });
                    task.Wait();
                });
        }

        private void ProcessByInput(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            var newName = (string)(argumentSet.Arguments.Length == 2 ? argumentSet.Arguments[1] : argumentSet.Arguments[0]);

            input.Subscribe(
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: o =>
                {
                    var task = Task.Run(async () =>
                    {
                        var converter = _toIdentifierConverterSelector.Select(o);
                        var results = converter(context, o, scope);
                        foreach (var result in results.ToEnumerable())
                        {
                            var renamedItem = await context.PathProcessor.Context.Logical.Nodes.Rename(result, newName, scope);
                            output.OnNext(renamedItem);
                        }
                    });
                    task.Wait();
                });
        }

        private IObservable<Identifier> ConvertPathToIds(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(pathSubject, scope, outputObserver);

                return Disposable.Empty;
            });

            return outputObservable
                .Select(o => context.ToIdentifierConverter.Convert(o))
                .ToHotObservable();
        }
    }
}