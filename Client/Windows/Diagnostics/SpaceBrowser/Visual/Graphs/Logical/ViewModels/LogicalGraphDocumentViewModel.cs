namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalGraphDocumentViewModel : GraphDocumentViewModelBase, ILogicalGraphDocumentViewModel
    {
        public LogicalGraphDocumentViewModel(
            IFabricContext fabric,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu, 
            IGraphContext graphContext)
            : base(fabric, buttons, contextMenu, graphContext)
        {
        }

        protected override void OnDrop(Identifier identifier)
        {
            GraphContext.CommandProcessor.Process(new RetrieveEntryCommand(identifier, ProcessReason.Retrieved), GraphContext.RetrieveEntryCommandHandler);
        }

        protected override void OnEntryStored(Identifier id)
        {
            if (Configuration.AddNewEntries)
            {
                GraphContext.CommandProcessor.Process(new RetrieveEntryCommand(id, ProcessReason.Retrieved), GraphContext.RetrieveEntryCommandHandler);
            }
            if (Configuration.ExpandNewEntries)
            {
                // Show any new entries.
            }
        }

        protected override void SelectEntry(object parameter)
        {
            // Select an entry.
        }

        protected override void DiscoverEntry(object parameter)
        {
            var entry = parameter as Entry;
            if (entry != null)
            {
                GraphContext.CommandProcessor.Process(new DiscoverEntryCommand(entry, ProcessReason.Discovered, 3), GraphContext.DiscoverEntryCommandHandler);
            }
        }
    }
}
