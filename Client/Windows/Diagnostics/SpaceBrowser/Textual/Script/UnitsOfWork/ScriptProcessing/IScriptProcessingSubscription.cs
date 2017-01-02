namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Workflow;

    public interface IScriptProcessingSubscription
    {
        void Subscribe(IObservable<SequenceProcessingResult> results, ScriptViewModel viewModel, List<TextualError> errors, DateTime start);

    }
}
