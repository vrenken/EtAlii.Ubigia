namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;

    internal class IdFunctionHandler : FunctionHandlerBase, IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public IdFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof(PathSubject))),
                new ParameterSet(false, new Parameter("var", typeof(IObservable<object>))),
            };
            Name = "Id";
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
                    //throw new ScriptProcessingException("Unable to convert arguments for rename function processing")
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
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: o =>
                {
                    var converter = ToIdentifierConverterSelector.Select(o);
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
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: o =>
                {
                    var converter = ToIdentifierConverterSelector.Select(o);
                    var results = converter(context, o, scope);
                    foreach (var result in results.ToEnumerable())
                    {
                        output.OnNext(result);
                    }
                });
        }
    }
}