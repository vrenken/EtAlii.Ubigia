namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.xTechnology.Workflow;

    public interface IScriptProcessingSubscription
    {
        void Subscribe(IObservable<SequenceProcessingResult> results, ScriptViewModel viewModel, List<TextualError> errors, DateTime start);

    }
}
