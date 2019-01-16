namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;

    public class GraphDocumentViewModel : GraphDocumentViewModelBase
    {
        public GraphDocumentViewModel(
            IFabricContext fabric,
            IGraphContext graphContext,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu)
            : base(fabric, buttons, contextMenu, graphContext)
        {
        }
    }
}
