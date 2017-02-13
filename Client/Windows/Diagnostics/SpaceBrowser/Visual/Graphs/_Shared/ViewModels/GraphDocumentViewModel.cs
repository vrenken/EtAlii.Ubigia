namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
