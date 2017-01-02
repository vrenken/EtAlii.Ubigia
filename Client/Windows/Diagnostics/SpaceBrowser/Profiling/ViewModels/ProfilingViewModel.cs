namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Mvvm;
    using EtAlii.xTechnology.Workflow;
    using Syncfusion.UI.Xaml.Charts;
    using ICommand = System.Windows.Input.ICommand;

    public class ProfilingViewModel : BindableBase, IProfilingViewModel
    {
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }
        private string _title;

        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public ReadOnlyObservableCollection<ProfilingResult> Results { get { return _results; } }
        private ReadOnlyObservableCollection<ProfilingResult> _results;

        public IProfilingAspectsViewModel Aspects {get {return _aspects; } }
        private readonly IProfilingAspectsViewModel _aspects;

        private readonly IProfileComposer _profileComposer;
        private readonly MainDispatcherInvoker _dispatcher;
        private ObservableCollection<ProfilingResult> _items;

        public System.Windows.Input.ICommand ClearCommand { get { return _clearCommand; } }
        private readonly ICommand _clearCommand;

        public bool AutoExpandNodes { get { return _autoExpandNodes; } set { base.SetProperty(ref _autoExpandNodes, value); } }
        private bool _autoExpandNodes;

        //public ScriptButtonsViewModel Buttons { get { return _buttons; } }
        //private readonly ScriptButtonsViewModel _buttons;

        public ProfilingViewModel(
            IUnitOfWorkProcessor unitOfWorkProcessor,
            IQueryProcessor queryProcessor, 
            IProfileComposer profileComposer,
            MainDispatcherInvoker dispatcher,
            IProfilingAspectsViewModel aspects
            )
        {
            _queryProcessor = queryProcessor;
            _profileComposer = profileComposer;
            _dispatcher = dispatcher;
            _aspects = aspects;
            _unitOfWorkProcessor = unitOfWorkProcessor;

            _items = new ObservableCollection<ProfilingResult>();
            _results = new ReadOnlyObservableCollection<ProfilingResult>(_items);

            ((INotifyCollectionChanged)_profileComposer.Results).CollectionChanged += ResultsOnCollectionChanged;

            _autoExpandNodes = true;
            _clearCommand = new RelayCommand(Clear, CanClear);
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
