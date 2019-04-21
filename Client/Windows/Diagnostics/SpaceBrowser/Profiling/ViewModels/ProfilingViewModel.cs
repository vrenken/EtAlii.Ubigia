namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.xTechnology.Mvvm;

    public class ProfilingViewModel : BindableBase, IProfilingViewModel
    {
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        private string _title;

        public ReadOnlyObservableCollection<ProfilingResult> Results { get; }

        public IProfilingAspectsViewModel Aspects { get; }

        private readonly IGraphContext _graphContext;
        private readonly IProfileComposer _profileComposer;
        private readonly IMainDispatcherInvoker _dispatcher;
        private readonly ObservableCollection<ProfilingResult> _items;

        public System.Windows.Input.ICommand ClearCommand { get; }

        public bool AutoExpandNodes { get => _autoExpandNodes; set => SetProperty(ref _autoExpandNodes, value); }
        private bool _autoExpandNodes;

        //public ScriptButtonsViewModel Buttons [ get [ return _buttons; ] ]
        //private readonly ScriptButtonsViewModel _buttons

        public ProfilingViewModel(
            IGraphContext graphContext,
            IProfileComposer profileComposer,
            IMainDispatcherInvoker dispatcher,
            IProfilingAspectsViewModel aspects
            )
        {
            _graphContext = graphContext;
            _profileComposer = profileComposer;
            _dispatcher = dispatcher;
            Aspects = aspects;

            _items = new ObservableCollection<ProfilingResult>();
            Results = new ReadOnlyObservableCollection<ProfilingResult>(_items);

            ((INotifyCollectionChanged)_profileComposer.Results).CollectionChanged += ResultsOnCollectionChanged;

            _autoExpandNodes = true;
            ClearCommand = new RelayCommand(Clear, CanClear);
        }

        private bool CanClear(object parameter)
        {
            return _profileComposer.Results.Any();
        }

        private void Clear(object parameter)
        {
            _profileComposer.Clear();
        }

        private void ResultsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _dispatcher.Invoke(() =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (var newItem in e.NewItems.Cast<ProfilingResult>())
                        {
                            _items.Add(newItem);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var oldItem in e.OldItems.Cast<ProfilingResult>())
                        {
                            _items.Add(oldItem);
                        }
                        break;
                        case NotifyCollectionChangedAction.Reset:
                        _items.Clear();
                        break;
                }
            });
        }
    }
}
