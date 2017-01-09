namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Workflow;

    public class ProcessScriptUnitOfworkHandler : UnitOfWorkHandlerBase<ProcessScriptUnitOfwork>, IProcessScriptUnitOfworkHandler
    {
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;
        private readonly IDataContext _dataContext;
        private readonly IMainDispatcherInvoker _dispatcherInvoker;
        private readonly IStatusScriptProcessingSubscription _statusScriptProcessingSubscription;
        private readonly IOutputScriptProcessingSubscription _outputScriptProcessingSubscription;
        private readonly IDiagnosticsScriptProcessingSubscription _diagnosticsScriptProcessingSubscription;


        public ProcessScriptUnitOfworkHandler(
            IUnitOfWorkProcessor unitOfWorkProcessor, 
            IDataContext dataContext,
            IMultiResultFactory resultFactory,
            IMainDispatcherInvoker dispatcherInvoker, 
            IStatusScriptProcessingSubscription statusScriptProcessingSubscription, 
            IDiagnosticsScriptProcessingSubscription diagnosticsScriptProcessingSubscription, 
            IOutputScriptProcessingSubscription outputScriptProcessingSubscription) 
        {
            _unitOfWorkProcessor = unitOfWorkProcessor;
            _dataContext = dataContext;
            _dispatcherInvoker = dispatcherInvoker;
            _statusScriptProcessingSubscription = statusScriptProcessingSubscription;
            _diagnosticsScriptProcessingSubscription = diagnosticsScriptProcessingSubscription;
            _outputScriptProcessingSubscription = outputScriptProcessingSubscription;
        }

        protected override void Handle(ProcessScriptUnitOfwork unitOfWork)
        {
            var viewModel = unitOfWork.ScriptViewModel;

            _unitOfWorkProcessor.Process(new ParseScriptUnitOfwork(viewModel));

            if (viewModel.CanExecute)
            {
                _dispatcherInvoker.Invoke(() =>
                {
                    viewModel.CanExecute = false;
                    viewModel.CanStop = true;
                    viewModel.ScriptResults = new ObservableCollection<Result>();
                    Task.Delay(100).Wait();
                });

                Task.Run(() =>
                {
                    var start = DateTime.Now;
                    var errors = new List<TextualError>();
                    var results = _dataContext.Scripts.Process(viewModel.Script, viewModel.Scope);

                    // First we subscribe our diagnostics observable hierarchy
                    //_diagnosticsScriptProcessingSubscription.Subscribe(results, viewModel, errors, start);
                    _outputScriptProcessingSubscription.Subscribe(results, viewModel, errors, start);
                    _statusScriptProcessingSubscription.Subscribe(results, viewModel, errors, start);
                });
            }
        }
    }
}
