namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Mvvm;
    using ICommand = System.Windows.Input.ICommand;

    public class GraphButtonsViewModel : BindableBase, IGraphButtonsViewModel
    {
        private readonly IFabricContext _fabric;

        public ICommand DiscoverFromHeadCommand { get { return _discoverFromHeadCommand; } }
        private readonly ICommand _discoverFromHeadCommand;

        public ICommand DiscoverFromTailCommand { get { return _discoverFromTailCommand; } }
        private readonly ICommand _discoverFromTailCommand;
        
        public ICommand ClearGraphCommand { get { return _clearGraphCommand; } }
        private readonly ICommand _clearGraphCommand;

        private readonly IGraphContext _graphContext;

        public GraphButtonsViewModel(
            IFabricContext fabric,
            IGraphContext graphContext)  
        {
            _fabric = fabric;
            _graphContext = graphContext;

            _discoverFromHeadCommand = new RelayCommand(DiscoverFromHead, CanDiscoverFromHead);
            _discoverFromTailCommand = new RelayCommand(DiscoverFromTail, CanDiscoverFromTail);
            _clearGraphCommand = new RelayCommand(ClearGraph, CanClearGraph);
        }

        private bool CanDiscoverFromHead(object parameter)
        {
            return parameter is IGraphDocumentViewModel;
        }

        private void DiscoverFromHead(object parameter)
        {
            var graphViewModel = parameter as IGraphDocumentViewModel;
            if (graphViewModel != null)
            {
                IReadOnlyEntry entry = null;
                var task = Task.Run(async () =>
                {
                    var root = await _fabric.Roots.Get(DefaultRoot.Head);
                    entry = await _fabric.Entries.Get(root, new ExecutionScope(false));
                });
                task.Wait();

                _graphContext.CommandProcessor.Process(new DiscoverEntryCommand(entry, ProcessReason.Discovered, 3), _graphContext.DiscoverEntryCommandHandler);
            }
        }

        private bool CanDiscoverFromTail(object parameter)
        {
            return parameter is IGraphDocumentViewModel;
        }

        private void DiscoverFromTail(object parameter)
        {
            var graphViewModel = parameter as IGraphDocumentViewModel;
            if (graphViewModel != null)
            {
                IReadOnlyEntry entry = null;
                var task = Task.Run(async () =>
                {
                    var root = await _fabric.Roots.Get(DefaultRoot.Tail);
                    entry = await _fabric.Entries.Get(root, new ExecutionScope(false));
                });
                task.Wait();

                _graphContext.CommandProcessor.Process(new DiscoverEntryCommand(entry, ProcessReason.Discovered, 3), _graphContext.DiscoverEntryCommandHandler);
            }
        }

        private bool CanClearGraph(object parameter)
        {
            var graphViewModel = parameter as IGraphDocumentViewModel;
            var canExecute = false;
            if (graphViewModel != null)
            {
                canExecute = true;// graphViewModel.NodesSource.Count() != 0;
            }
            return canExecute;
        }

        private void ClearGraph(object parameter)
        {
            var graphViewModel = parameter as GraphDocumentViewModel;
            graphViewModel.Clear();
        }

    }
}
