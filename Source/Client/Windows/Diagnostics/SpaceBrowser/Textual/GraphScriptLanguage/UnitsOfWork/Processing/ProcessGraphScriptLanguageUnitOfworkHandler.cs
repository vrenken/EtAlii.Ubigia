namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.Structure.Workflow;

    public class ProcessGraphScriptLanguageUnitOfworkHandler : UnitOfWorkHandlerBase<ProcessGraphScriptLanguageUnitOfwork>, IProcessGraphScriptLanguageUnitOfworkHandler
    {
        private readonly IGraphContext _graphContext;
        private readonly IGraphSLScriptContext _scriptContext;
        private readonly IMainDispatcherInvoker _dispatcherInvoker;
        private readonly IStatusGraphScriptLanguageProcessingSubscription _statusGraphScriptLanguageProcessingSubscription;
        private readonly IOutputGraphScriptLanguageProcessingSubscription _outputGraphScriptLanguageProcessingSubscription;
        //private readonly IDiagnosticsGraphScriptLanguageProcessingSubscription _diagnosticsGraphScriptLanguageProcessingSubscription
        private readonly IParseGraphScriptLanguageUnitOfworkHandler _parseScriptUnitOfworkHandler;

        public ProcessGraphScriptLanguageUnitOfworkHandler(
            //IMultiResultFactory resultFactory,
            IMainDispatcherInvoker dispatcherInvoker, 
            IStatusGraphScriptLanguageProcessingSubscription statusGraphScriptLanguageProcessingSubscription, 
            //IDiagnosticsGraphScriptLanguageProcessingSubscription diagnosticsGraphScriptLanguageProcessingSubscription 
            IOutputGraphScriptLanguageProcessingSubscription outputGraphScriptLanguageProcessingSubscription, 
            IParseGraphScriptLanguageUnitOfworkHandler parseScriptUnitOfworkHandler, 
            IGraphContext graphContext, 
            IGraphSLScriptContext scriptContext) 
        {
            _dispatcherInvoker = dispatcherInvoker;
            _statusGraphScriptLanguageProcessingSubscription = statusGraphScriptLanguageProcessingSubscription;
            //_diagnosticsGraphScriptLanguageProcessingSubscription = diagnosticsGraphScriptLanguageProcessingSubscription
            _outputGraphScriptLanguageProcessingSubscription = outputGraphScriptLanguageProcessingSubscription;
            _parseScriptUnitOfworkHandler = parseScriptUnitOfworkHandler;
            _graphContext = graphContext;
            _scriptContext = scriptContext;
        }

        protected override void Handle(ProcessGraphScriptLanguageUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.ScriptViewModel;

            _graphContext.UnitOfWorkProcessor.Process(new ParseGraphScriptLanguageUnitOfwork(viewModel), _parseScriptUnitOfworkHandler);

            if (viewModel.CanExecute)
            {
                _dispatcherInvoker.Invoke(() =>
                {
                    viewModel.CanExecute = false;
                    viewModel.CanStop = true;
                    viewModel.ScriptResults.Clear();
                    Task.Delay(100).Wait();
                });

                Task.Run(() =>
                {
                    var start = DateTime.Now;
                    var errors = new List<TextualError>();
                    var results = _scriptContext.Process(viewModel.Script, viewModel.Scope);

                    // First we subscribe our diagnostics observable hierarchy
                    //_diagnosticsGraphScriptLanguageProcessingSubscription.Subscribe(results, viewModel, errors, start)
                    _outputGraphScriptLanguageProcessingSubscription.Subscribe(results, viewModel, errors, start);
                    _statusGraphScriptLanguageProcessingSubscription.Subscribe(results, viewModel, errors, start);
                });
            }
        }
    }
}
