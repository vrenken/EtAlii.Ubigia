namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class IdFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
        private readonly ParameterSet[] _parameterSets;

        public string Name { get { return _name; } }
        private readonly string _name;

        private readonly ISelector<object, Func<IFunctionContext, object, ExecutionScope, IObservable<Identifier>>> _toIdentifierConverterSelector;

        public IdFunctionHandler()
        {
            _parameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof(PathSubject))),
                new ParameterSet(false, new Parameter("var", typeof(IObservable<object>))),
            };
            _name = "Id";

            _toIdentifierConverterSelector = new Selector<object, Func<IFunctionContext, object, ExecutionScope, IObservable<Identifier>>>()
                .Register(i => i is PathSubject, (c, i, s) => ConvertPathToIds(c, (PathSubject)i, s))
                .Register(i => i is Identifier, (c, i, s) => Observable.Return((Identifier)i))
                .Register(i => i is IInternalNode, (c, i, s) => Observable.Return(((IInternalNode)i).Id))
                .Register(i => true, (c, i, s) => { throw new ScriptProcessingException("Unable to convert input for Id function processing"); });
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            if (processAsSubject)
            {
                if (argumentSet.Arguments.Length == 1)
                {
                    ProcessByArgument(context, parameterSet, argumentSet, scope, output);
                }
                else
                {
                    // No way to throw an exception here. It could be a left side subject so we will have to wait until it is executed from an operator.
                    //throw new ScriptProcessingException("Unable to convert arguments for rename function processing");
                    output.OnCompleted();
                }
            }
            else
            {
                if (argumentSet.Arguments.Length == 0)
                {
                    ProcessByInput(context, parameterSet, argumentSet, input, scope, output);
                }
                else
                {
                    throw new ScriptProcessingException("Unable to convert arguments and input for Id function processing");
                }
            }
        }

        private void ProcessByArgument(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, ExecutionScope scope, IObserver<object> output)
        {
            var input = argumentSet.Arguments[0] as IObservable<object>;
            if (input == null)
            {
                throw new ScriptProcessingException("Unable to convert arguments for Id function processing");
            }
            input.Subscribe(
                onError: (e) => output.OnError(e),
                onCompleted: () => output.OnCompleted(),
                onNext: o =>
                {
                    var converter = _toIdentifierConverterSelector.Select(o);
                    var results = converter(context, o, scope);
                    foreach (var result in results.ToEnumerable())
                    {
                        output.OnNext(result);
                    }
                });
        }

        private void ProcessByInput(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            input.Subscribe(
                onError: (e) => output.OnError(e),
                onCompleted: () => output.OnCompleted(),
                onNext: o =>
                {
                    var converter = _toIdentifierConverterSelector.Select(o);
                    var results = converter(context, o, scope);
                    foreach (var result in results.ToEnumerable())
                    {
                        output.OnNext(result);
                    }
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