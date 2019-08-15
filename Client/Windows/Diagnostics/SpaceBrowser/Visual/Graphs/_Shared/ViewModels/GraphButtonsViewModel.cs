namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Windows.Mvvm;

    public class GraphButtonsViewModel : BindableBase, IGraphButtonsViewModel
    {
        private readonly IFabricContext _fabric;

        public ICommand DiscoverFromHeadCommand { get; }

        public ICommand DiscoverFromTailCommand { get; }

        public ICommand ClearGraphCommand { get; }

        private readonly IGraphContext _graphContext;

        public GraphButtonsViewModel(
            IFabricContext fabric,
            IGraphContext graphContext)  
        {
            _fabric = fabric;
            _graphContext = graphContext;

            DiscoverFromHeadCommand = new RelayCommand(DiscoverFromHead, CanDiscoverFromHead);
            DiscoverFromTailCommand = new RelayCommand(DiscoverFromTail, CanDiscoverFromTail);
            ClearGraphCommand = new RelayCommand(ClearGraph, CanClearGraph);
        }

        private bool CanDiscoverFromHead(object parameter)
        {
            return parameter is IGraphDocumentViewModel;
        }

        private void DiscoverFromHead(object parameter)
        {
            if (parameter is IGraphDocumentViewModel)
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
            var canExecute = parameter is IGraphDocumentViewModel;
            return canExecute;
        }

        private void ClearGraph(object parameter)
        {
            var graphViewModel = parameter as GraphDocumentViewModel;
            graphViewModel.Clear();
        }

    }
}
