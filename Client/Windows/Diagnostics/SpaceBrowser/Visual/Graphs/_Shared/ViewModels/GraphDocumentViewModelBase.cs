namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using Northwoods.GoXam.Model;

//using System.Windows.Input

    public partial class GraphDocumentViewModelBase : GraphLinksModel<EntryNode, Identifier, string, EntryLink>, IGraphDocumentViewModel
    {
        protected IFabricContext Fabric { get; }

        public IGraphConfiguration Configuration => GraphContext.Configuration;

        public IGraphButtonsViewModel Buttons { get; }

        public IGraphContextMenuViewModel ContextMenu { get; }

        public IGraphContext GraphContext { get; }

        public string Title { get => _title; set { if (_title != value) { var old = _title; _title = value; RaisePropertyChanged("Title", this, old, value); } } }
        private string _title;

        public GraphDocumentViewModelBase(
            IFabricContext fabric,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu, 
            IGraphContext graphContext)
        {
            Fabric = fabric;
            Buttons = buttons;
            ContextMenu = contextMenu;
            GraphContext = graphContext;

            //_commandQueue.Register<RetrieveEntryCommand, RetrieveEntryCommandHandler>()
            //_commandQueue.Register<ProcessEntryCommand, ProcessEntryCommandHandler>()
            //_commandQueue.Register<AddEntryToGraphCommand, AddEntryToGraphCommandHandler>(handler => handler.GraphViewModel = this)
            //_commandQueue.Register<AddEntryRelationsToGraphCommand, AddEntryRelationsToGraphCommandHandler>(handler => handler.GraphViewModel = this)
            //_commandQueue.Register<DiscoverEntryCommand, DiscoverEntryCommandHandler>(handler => handler.GraphViewModel = this)

            //_fabric.Entries.Prepared += OnEntryPrepared
            Fabric.Entries.Stored += OnEntryStored;

            //_discoverEntryCommand = new RelayCommand(DiscoverEntry)
            //_selectEntryCommand = new RelayCommand(SelectEntry)

            Clear();

            Modifiable = true;
        }

        //private void OnEntryPrepared(Identifier identifier)
        //[
        //]
        protected virtual void OnEntryStored(Identifier id)
        {
            // Handle an entry store event.
        }

        protected virtual void SelectEntry(object parameter)
        {
            // Handle an entry selected event.
        }

        protected virtual void DiscoverEntry(object parameter)
        {
            // Handle an entry discovered event.
        }

        internal void Clear()
        {
            LinksSource = new ObservableCollection<EntryLink>();
            NodesSource = new ObservableCollection<EntryNode>();
        }
    }
}
