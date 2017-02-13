namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;

    public class ViewFunctionHandler : FunctionHandlerBase, IViewFunctionHandler
    {
        private readonly IDocumentsProvider _documentsProvider;

        public ParameterSet[] ParameterSets { get; }
        public string Name { get; }

        public ViewFunctionHandler(
            IDocumentsProvider documentsProvider)
        {
            _documentsProvider = documentsProvider;

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
                    var name = argumentSet.Arguments[0] as string;
                    var document = _documentsProvider.Documents.SingleOrDefault(d => d.Title == name);
                    if (document == null)
                    {
                        throw new ScriptProcessingException($"No view found with name: '{name}'");
                    }
                    else
                    {
                        ProcessByInput(context, parameterSet, argumentSet, input, scope, output);
                    }
                }
                else
                {
                    throw new ScriptProcessingException("Unable to convert arguments and input for View function processing");
                }
            }
        }


        private void ProcessByInput(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            var name = argumentSet.Arguments[0] as string;
            var document = _documentsProvider.Documents.SingleOrDefault(d => d.Title == name);
            var graphDocumentViewModel = document as IGraphDocumentViewModel;

            input.Subscribe(
                onError: (e) => output.OnError(e),
                onCompleted: () => output.OnCompleted(),
                onNext: o =>
                {
                    if (graphDocumentViewModel != null)
                    {
                        var converter = ToIdentifierConverterSelector.Select(o);
                        var results = converter(context, o, scope);
                        foreach (var result in results.ToEnumerable())
                        {
                            graphDocumentViewModel.GraphContext.CommandProcessor.Process(new RetrieveEntryCommand(result, ProcessReason.Retrieved), graphDocumentViewModel.GraphContext.RetrieveEntryCommandHandler);
                        }
                    }
                    output.OnNext(o);
                });
        }
    }
}