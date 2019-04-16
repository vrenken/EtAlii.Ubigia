namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;

    public class DiagnosticsGraphQueryLanguageProcessingSubscription : IDiagnosticsGraphQueryLanguageProcessingSubscription
    {
        private readonly IStatusWriter _statusWriter;


        public DiagnosticsGraphQueryLanguageProcessingSubscription(IStatusWriter statusWriter)
        {
            _statusWriter = statusWriter;
        }

        public void Subscribe(IObservable<QueryProcessingResult> results, IGraphQueryLanguageViewModel viewModel, List<TextualError> errors, DateTime start)
        {
            results
                //.ObserveOn(NewThreadScheduler.Default)
                //.SubscribeOnDispatcher()
                .Subscribe(
                    onError: e => _statusWriter.Write(viewModel, $"Query error: {e.Message}"),
                    onFirst: () => _statusWriter.Write(viewModel, "Query start"),
                    onNext: o =>
                    {
//                        _statusWriter.Write(viewModel, $"Query: {o.Sequence} ({o.Step}/{o.Total})");
//                        o.Output
//                            //.ObserveOn(NewThreadScheduler.Default)
//                            //.SubscribeOnDispatcher()
//                            .Subscribe(
//                                onError: e2 => _statusWriter.Write(viewModel, $"   Result error: {e2.Message}"),
//                                onFirst: () => _statusWriter.Write(viewModel, "   Result start"),
//                                onNext: o2 => _statusWriter.Write(viewModel, $"   Result next: {o2.GetType()}"),
//                                onCompleted: () => _statusWriter.Write(viewModel, "   Result completed"));
                    },
                    onCompleted: () => _statusWriter.Write(viewModel, "Query completed"));
        }
    }
}