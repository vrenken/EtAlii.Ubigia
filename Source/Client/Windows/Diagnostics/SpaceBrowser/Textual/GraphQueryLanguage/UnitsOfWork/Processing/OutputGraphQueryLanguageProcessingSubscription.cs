namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Querying;
    using EtAlii.Ubigia.Api.Logical;

    public class OutputGraphQueryLanguageProcessingSubscription : IOutputGraphQueryLanguageProcessingSubscription
    {
        //private readonly IErrorWriter _errorWriter
        //private readonly IMultiResultFactory _resultFactory


//        public OutputGraphQueryLanguageProcessingSubscription(
//            //IErrorWriter errorWriter, 
//            //IMultiResultFactory resultFactory
//            )
//        [
//            //_errorWriter = errorWriter
//            //_resultFactory = resultFactory
//        ]

        public void Subscribe(
            IObservable<GraphQLQueryProcessingResult> results, 
            IGraphQueryLanguageViewModel viewModel, 
            List<TextualError> errors, DateTime start)
        {
            //var outputIndex = 1

            results.Subscribe(
                    onError: e => viewModel.QueryResult = e.Message,
                    onFirst: () => viewModel.QueryResult = string.Empty,
                    onNext: o => viewModel.QueryResult = o.DataAsString,
                    onCompleted: () => { });
        }
    }
}