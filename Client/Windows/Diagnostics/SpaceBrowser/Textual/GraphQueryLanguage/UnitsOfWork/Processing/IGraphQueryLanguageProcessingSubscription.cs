namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional;

    public interface IGraphQueryLanguageProcessingSubscription
    {
        void Subscribe(
            IObservable<GraphQLQueryProcessingResult> results, 
            IGraphQueryLanguageViewModel viewModel, 
            List<TextualError> errors, DateTime start);

    }
}
