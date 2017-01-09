namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Client.Windows.Shared;
    using EtAlii.Ubigia.Windows;
    using EtAlii.xTechnology.Workflow;
    using Northwoods.GoXam.Model;
    using ICommand = System.Windows.Input.ICommand;

    public partial class GraphDocumentViewModelBase : GraphLinksModel<EntryNode, Identifier, string, EntryLink>, IGraphDocumentViewModel
    {
        #region Properties

        protected IFabricContext Fabric { get { return _fabric; } }
        private readonly IFabricContext _fabric;

        public ICommandProcessor CommandProcessor { get { return _commandProcessor; } }
        private readonly ICommandProcessor _commandProcessor;

        private readonly ICommand _selectEntryCommand;
        private readonly ICommand _discoverEntryCommand;

        public IGraphConfiguration Configuration { get { return _configuration; } }
        private readonly IGraphConfiguration _configuration;

        public IGraphButtonsViewModel Buttons { get { return _buttons; } }
        private readonly IGraphButtonsViewModel _buttons;

        public IGraphContextMenuViewModel ContextMenu { get { return _contextMenu; } }
        private readonly IGraphContextMenuViewModel _contextMenu;

        public string Title 
        {
            get { return _title; }
            set { if (_title != value) { var old = _title; _title = value; RaisePropertyChanged("Title", this, old, value); } }
        }
        private string _title;

        #endregion

        public GraphDocumentViewModelBase(
            IFabricContext fabric,
            ICommandProcessor commandProcessor,
            IGraphConfiguration configuration,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu)
        {
            _fabric = fabric;
            _commandProcessor = commandProcessor;
            _configuration = configuration;
            _buttons = buttons;
            _contextMenu = contextMenu;

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

        private void OnEntryStored(Identifier id)
        {
            if (Configuration.AddNewEntries)
            {
                _commandProcessor.Process(new RetrieveEntryCommand(id, ProcessReason.Retrieved));
            }
            if (Configuration.ExpandNewEntries)
            {
            }
        }
        
        private void SelectEntry(object parameter)
        {
        }

        private void DiscoverEntry(object parameter)
        {
            var entry = parameter as Entry;
            if (entry != null)
            {
                _commandProcessor.Process(new DiscoverEntryCommand(entry, ProcessReason.Discovered, 3));
            }
        }

        internal void Clear()
        {
            LinksSource = new ObservableCollection<EntryLink>();
            NodesSource = new ObservableCollection<EntryNode>();
        }
    }
}
