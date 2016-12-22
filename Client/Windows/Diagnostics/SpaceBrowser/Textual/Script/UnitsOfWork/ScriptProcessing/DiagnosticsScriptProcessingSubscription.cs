namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Functional;

    public class DiagnosticsScriptProcessingSubscription : IDiagnosticsScriptProcessingSubscription
    {
        private readonly IStatusWriter _statusWriter;


        public DiagnosticsScriptProcessingSubscription(IStatusWriter statusWriter)
        {
            _statusWriter = statusWriter;
        }

        public void Subscribe(IObservable<SequenceProcessingResult> results, ScriptViewModel viewModel, List<TextualError> errors, DateTime start)
        {
            results
                //.ObserveOn(NewThreadScheduler.Default)
                //.SubscribeOnDispatcher()
                .Subscribe(
                    onError: e => _statusWriter.Write(viewModel, $"Sequence error: {e.Message}"),
                    onFirst: () => _statusWriter.Write(viewModel, "Sequence start"),
                    onNext: o =>
                    {
                        _statusWriter.Write(viewModel, $"Sequence: {o.Sequence} ({o.Step}/{o.Total})");
                        o.Output
                            //.ObserveOn(NewThreadScheduler.Default)
                            //.SubscribeOnDispatcher()
                            .Subscribe(
                                onError: e2 => _statusWriter.Write(viewModel, $"   Result error: {e2.Message}"),
                                onFirst: () => _statusWriter.Write(viewModel, "   Result start"),
                                onNext: o2 => _statusWriter.Write(viewModel, $"   Result next: {o2.GetType()}"),
                                onCompleted: () => _statusWriter.Write(viewModel, "   Result completed"));
                    },
                    onCompleted: () => _statusWriter.Write(viewModel, "Sequence completed"));
        }
    }
}