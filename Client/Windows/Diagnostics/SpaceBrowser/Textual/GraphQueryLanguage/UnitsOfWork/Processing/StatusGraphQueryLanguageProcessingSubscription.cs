namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class StatusGraphQueryLanguageProcessingSubscription : IStatusGraphQueryLanguageProcessingSubscription
    {
        private readonly IStatusWriter _statusWriter;
        private readonly IErrorWriter _errorWriter;

        public StatusGraphQueryLanguageProcessingSubscription(
            IStatusWriter statusWriter, 
            IErrorWriter errorWriter)
        {
            _statusWriter = statusWriter;
            _errorWriter = errorWriter;
        }

        public void Subscribe(IObservable<QueryProcessingResult> results, IGraphQueryLanguageViewModel viewModel,
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
                    viewModel.CanExecute = false;
                    viewModel.CanStop = true;
                    
//                    _statusWriter.Write(viewModel, $"Querying: [o.Sequence] (step [o.Step + 1]/[o.Total])")
//                    o.Output.Subscribe(
//                        onError: e2 => _errorWriter.Write(viewModel, e2, errors),
//                        onNext: o2 => [ ])
                },
                onCompleted: () =>
                {
                    viewModel.Errors = errors.ToArray();
                    if (errors.Count == 0)
                    {
                        var duration = DateTime.Now - start;
                        Task.Delay(200).Wait();
                        _statusWriter.Write(viewModel, "Query execution finished successful.");
                        _statusWriter.Write(viewModel, $"Total run time: {duration:hh}:{duration:mm}:{duration:ss}.{duration:fff}");
                    }

                    Task.Delay(500).Wait();
                    viewModel.CanStop = false;
                    viewModel.CanExecute = true;
                });
//
//            // And we want to disable the buttons at the first result.
//            results.FirstOrDefaultAsync()
//                .Subscribe(o =>
//                [
//                    viewModel.CanExecute = false
//                    viewModel.CanStop = true
//                ])
//
//            // We want to give a summary and reactivate the buttons after script processing has ended.
//            results.LastOrDefaultAsync()
//                .Subscribe(
//                onNext: o => [ ],
//                onCompleted: () =>
//                [
//                    viewModel.Errors = errors.ToArray()
//                    if [errors.Count == 0]
//                    [
//                        var duration = DateTime.Now - start
//                        Task.Delay(200).Wait()
//                        _statusWriter.Write(viewModel, "Query execution finished successful.")
//                        _statusWriter.Write(viewModel,
//                            $"Total run time: [duration:hh]:[duration:mm]:[duration:ss].[duration:fff]")
//                    ]
//
//                    Task.Delay(500).Wait()
//                    viewModel.CanStop = false
//                    viewModel.CanExecute = true
//                ])
        }
    }
}