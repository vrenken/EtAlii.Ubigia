namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Windows;
    using Northwoods.GoXam.Model;
    using ICommand = System.Windows.Input.ICommand;

    public partial class GraphDocumentViewModelBase : GraphLinksModel<EntryNode, Identifier, string, EntryLink>, IGraphDocumentViewModel
    {
        #region Properties

        protected IFabricContext Fabric { get { return _fabric; } }
        private readonly IFabricContext _fabric;

        private readonly ICommand _selectEntryCommand;
        private readonly ICommand _discoverEntryCommand;

        public IGraphConfiguration Configuration { get { return _graphContext.Configuration; } }

        public IGraphButtonsViewModel Buttons { get { return _buttons; } }
        private readonly IGraphButtonsViewModel _buttons;

        public IGraphContextMenuViewModel ContextMenu { get { return _contextMenu; } }
        private readonly IGraphContextMenuViewModel _contextMenu;

        public IGraphContext GraphContext => _graphContext;
        private readonly IGraphContext _graphContext;

        public string Title 
        {
            get { return _title; }
            set { if (_title != value) { var old = _title; _title = value; RaisePropertyChanged("Title", this, old, value); } }
        }
        private string _title;

        #endregion

        public GraphDocumentViewModelBase(
            IFabricContext fabric,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu, 
            IGraphContext graphContext)
        {
            _fabric = fabric;
            _buttons = buttons;
            _contextMenu = contextMenu;
            _graphContext = graphContext;

            //_commandQueue.Register<RetrieveEntryCommand, RetrieveEntryCommandHandler>();
            //_commandQueue.Register<ProcessEntryCommand, ProcessEntryCommandHandler>();
            //_commandQueue.Register<AddEntryToGraphCommand, AddEntryToGraphCommandHandler>(handler => handler.GraphViewModel = this);
            //_commandQueue.Register<AddEntryRelationsToGraphCommand, AddEntryRelationsToGraphCommandHandler>(handler => handler.GraphViewModel = this);
            //_commandQueue.Register<DiscoverEntryCommand, DiscoverEntryCommandHandler>(handler => handler.GraphViewModel = this);

            //_fabric.Entries.Prepared += OnEntryPrepared;
            _fabric.Entries.Stored += OnEntryStored;

            _discoverEntryCommand = new RelayCommand(DiscoverEntry);
            _selectEntryCommand = new RelayCommand(SelectEntry);

            Clear();

            Modifiable = true;
        }

        //private void OnEntryPrepared(Identifier identifier)
        //{
        //}

        protected virtual void OnEntryStored(Identifier id)
        {
        }

        protected virtual void SelectEntry(object parameter)
        {
        }

        protected virtual void DiscoverEntry(object parameter)
        {
        }

        internal void Clear()
        {
            LinksSource = new ObservableCollection<EntryLink>();
            NodesSource = new ObservableCollection<EntryNode>();
        }
    }
}
