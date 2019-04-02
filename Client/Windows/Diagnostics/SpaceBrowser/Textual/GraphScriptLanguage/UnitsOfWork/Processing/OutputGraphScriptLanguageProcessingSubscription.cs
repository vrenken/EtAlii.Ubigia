namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;

    public class OutputGraphScriptLanguageProcessingSubscription : IOutputGraphScriptLanguageProcessingSubscription
    {
        private readonly IErrorWriter _errorWriter;
        private readonly IMultiResultFactory _resultFactory;


        public OutputGraphScriptLanguageProcessingSubscription(
            IErrorWriter errorWriter, 
            IMultiResultFactory resultFactory)
        {
            _errorWriter = errorWriter;
            _resultFactory = resultFactory;
        }

        public void Subscribe(
            IObservable<SequenceProcessingResult> results, 
            IGraphScriptLanguageViewModel viewModel, 
            List<TextualError> errors, DateTime start)
        {
            var outputIndex = 1;

            results.Subscribe(
                    onError: e => { },
                    onFirst: () => viewModel.ScriptResults.Clear(),
                    onNext: o =>
                    {
                        var outputGroup = $"{outputIndex++:000}: {o.Sequence.ToString()}";
                        var groupSpacerToRemove = viewModel.ScriptResults.Count;
                        viewModel.ScriptResults.Add(new Result(null, null, null, outputGroup));
                        o.Output
                            .SelectMany(o2 => _resultFactory.Convert(o2, outputGroup))
                            .Subscribe(
                                onError: e2 => _errorWriter.Write(viewModel, e2, errors),
                                onFirst: () => viewModel.ScriptResults.RemoveAt(groupSpacerToRemove),
                                onNext: o2 => viewModel.ScriptResults.Add(o2),
                                onCompleted: () => { });
                    },
                    onCompleted: () =>
                    {
                        viewModel.ScriptVariables.Clear();
                        foreach (var kvp in viewModel.Scope.Variables)
                        {
                            kvp.Value.Value
                                .SelectMany(o2 => _resultFactory.Convert(o2, $"${kvp.Key}"))
                                .Subscribe(o3 => viewModel.ScriptVariables.Add(o3));
                        }
                    });
        }
    }
}