namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional;

    public interface IScriptProcessingSubscription
    {
        void Subscribe(IObservable<SequenceProcessingResult> results, IScriptViewModel viewModel, List<TextualError> errors, DateTime start);

    }
}
