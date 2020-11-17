namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class StatusGraphScriptLanguageProcessingSubscription : IStatusGraphScriptLanguageProcessingSubscription
    {
        private readonly IStatusWriter _statusWriter;
        private readonly IErrorWriter _errorWriter;


        public StatusGraphScriptLanguageProcessingSubscription(
            IStatusWriter statusWriter, 
            IErrorWriter errorWriter)
        {
            _statusWriter = statusWriter;
            _errorWriter = errorWriter;
        }

        public void Subscribe(IObservable<SequenceProcessingResult> results, IGraphScriptLanguageViewModel viewModel,
            List<TextualError> errors, DateTime start)
        {
            // We want to show all errors and all results.
            results.Subscribe(
                onError: e =>
                {
                    _errorWriter.Write(viewModel, e, errors);
                    Task.Delay(500).Wait();
                    viewModel.CanStop = false;
                    viewModel.CanExecute = true;
                },
                onNext: o =>
                {
                    _statusWriter.Write(viewModel, $"Executing: {o.Sequence} (step {o.Step + 1}/{o.Total})");
                    o.Output.Subscribe(
                        onError: e2 =>
                        {
                            _errorWriter.Write(viewModel, e2, errors);
                            Task.Delay(500).Wait();
                            viewModel.CanStop = false;
                            viewModel.CanExecute = true;
                            
                        },
                        onNext: _ => { });
                });

            // And we want to disable the buttons at the first result.
            results.FirstOrDefaultAsync()
                .Subscribe(_ =>
                {
                    viewModel.CanExecute = false;
                    viewModel.CanStop = true;
                });

            // We want to give a summary and reactivate the buttons after script processing has ended.
            results.LastOrDefaultAsync()
                .Subscribe(
                onNext: o =>
                {
                    o.Output
                        .Subscribe(
                            onNext: _ => { },
                            onCompleted: () =>
                            {
                                viewModel.Errors = errors.ToArray();
                                if (errors.Count == 0)
                                {
                                    var duration = DateTime.Now - start;
                                    Task.Delay(200).Wait();
                                    _statusWriter.Write(viewModel, "Script execution finished successful.");
                                    _statusWriter.Write(viewModel,
                                        $"Total run time: {duration:hh}:{duration:mm}:{duration:ss}.{duration:fff}");
                                }

                                Task.Delay(500).Wait();
                                viewModel.CanStop = false;
                                viewModel.CanExecute = true;
                            });
                },
                onCompleted: () => { });
        }
    }
}