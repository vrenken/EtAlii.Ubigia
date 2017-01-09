namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;

    public class ViewFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }
        public string Name { get; }

        public ViewFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true, new Parameter("view", typeof(string))),
            };

            Name = "View";
        }

        public void Process(
            IFunctionContext context, 
            ParameterSet parameterSet, 
            ArgumentSet argumentSet, 
            IObservable<object> input,
            ExecutionScope scope, 
            IObserver<object> output, 
            bool processAsSubject)
        {
            if (processAsSubject)
            {
                // No way to throw an exception here. It could be a left side subject so we will have to wait until it is executed from an operator.
                //throw new ScriptProcessingException("Unable to convert arguments for rename function processing");
                output.OnCompleted();
            }
            else
            {
                if (argumentSet.Arguments.Length == 1)
                {
                    ProcessByInput(context, parameterSet, argumentSet, input, scope, output);
                }
                else
                {
                    throw new ScriptProcessingException("Unable to convert arguments and input for View function processing");
                }
            }
        }


        private void ProcessByInput(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            input.Subscribe(
                onError: (e) => output.OnError(e),
                onCompleted: () => output.OnCompleted(),
                onNext: o =>
                {
                    //var converter = _toIdentifierConverterSelector.Select(o);
                    //var results = converter(context, o, scope);
                    //foreach (var result in results.ToEnumerable())
                    //{
                    //    output.OnNext(result);
                    //}
                });
        }

    }
}