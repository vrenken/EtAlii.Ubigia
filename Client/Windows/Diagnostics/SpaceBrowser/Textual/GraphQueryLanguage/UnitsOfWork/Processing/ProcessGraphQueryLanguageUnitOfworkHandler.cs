namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Workflow;

    public class ProcessGraphQueryLanguageUnitOfworkHandler : UnitOfWorkHandlerBase<ProcessGraphQueryLanguageUnitOfwork>, IProcessGraphQueryLanguageUnitOfworkHandler
    {
        private readonly IGraphContext _graphContext;
        private readonly IGraphQLQueryContext _queryContext;
        private readonly IMainDispatcherInvoker _dispatcherInvoker;
        private readonly IStatusGraphQueryLanguageProcessingSubscription _statusGraphQueryLanguageProcessingSubscription;
        private readonly IOutputGraphQueryLanguageProcessingSubscription _outputGraphQueryLanguageProcessingSubscription;
        private readonly IDiagnosticsGraphQueryLanguageProcessingSubscription _diagnosticsGraphQueryLanguageProcessingSubscription;
        private readonly IParseGraphQueryLanguageUnitOfworkHandler _parseQueryUnitOfworkHandler;

        public ProcessGraphQueryLanguageUnitOfworkHandler(
            IGraphQLQueryContext queryContext,
            //IMultiResultFactory resultFactory,
            IMainDispatcherInvoker dispatcherInvoker, 
            IStatusGraphQueryLanguageProcessingSubscription statusGraphQueryLanguageProcessingSubscription, 
            IDiagnosticsGraphQueryLanguageProcessingSubscription diagnosticsGraphQueryLanguageProcessingSubscription, 
            IOutputGraphQueryLanguageProcessingSubscription outputGraphQueryLanguageProcessingSubscription, 
            IParseGraphQueryLanguageUnitOfworkHandler parseQueryUnitOfworkHandler, 
            IGraphContext graphContext) 
        {
            _queryContext = queryContext;
            _dispatcherInvoker = dispatcherInvoker;
            _statusGraphQueryLanguageProcessingSubscription = statusGraphQueryLanguageProcessingSubscription;
            _diagnosticsGraphQueryLanguageProcessingSubscription = diagnosticsGraphQueryLanguageProcessingSubscription;
            _outputGraphQueryLanguageProcessingSubscription = outputGraphQueryLanguageProcessingSubscription;
            _parseQueryUnitOfworkHandler = parseQueryUnitOfworkHandler;
            _graphContext = graphContext;
        }

        protected override void Handle(ProcessGraphQueryLanguageUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.QueryViewModel;

            _graphContext.UnitOfWorkProcessor.Process(new ParseGraphQueryLanguageUnitOfwork(viewModel), _parseQueryUnitOfworkHandler);

            if (viewModel.CanExecute)
            {
                _dispatcherInvoker.Invoke(() =>
                {
                    viewModel.CanExecute = false;
                    viewModel.CanStop = true;
                    viewModel.QueryResult = string.Empty;
                    Task.Delay(100).Wait();
                });

                Task.Run(() =>
                {
                    var start = DateTime.Now;
                    var errors = new List<TextualError>();

                    var results = Observable.Create<QueryProcessingResult>(async output =>
                    {
                        var queryExecutionResults = await _queryContext.Process(viewModel.Query); //, viewModel.Scope)

                        output.OnNext(queryExecutionResults);
                        return System.Reactive.Disposables.Disposable.Empty;
                    }).ToHotObservable();

                    // First we subscribe our diagnostics observable hierarchy
                    //_diagnosticsGraphQueryLanguageProcessingSubscription.Subscribe(results, viewModel, errors, start)
                    _outputGraphQueryLanguageProcessingSubscription.Subscribe(results, viewModel, errors, start);
                    _statusGraphQueryLanguageProcessingSubscription.Subscribe(results, viewModel, errors, start);
                });
            }
        }
    }
}
