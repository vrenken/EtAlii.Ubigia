namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface IGraphScriptLanguageProcessingSubscription
    {
        void Subscribe(
            IObservable<SequenceProcessingResult> results, 
            IGraphScriptLanguageViewModel viewModel, 
            List<TextualError> errors, DateTime start);

    }
}
