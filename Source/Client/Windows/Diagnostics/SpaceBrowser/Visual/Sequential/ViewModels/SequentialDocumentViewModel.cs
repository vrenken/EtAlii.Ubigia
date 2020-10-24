namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;

    public class SequentialDocumentViewModel : GraphDocumentViewModelBase, ISequentialDocumentViewModel
    {
        public SequentialDocumentViewModel(
            IFabricContext fabric,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu,
            IGraphContext graphContext)
            : base(fabric, buttons, contextMenu, graphContext)
        {
        }
    }
}
