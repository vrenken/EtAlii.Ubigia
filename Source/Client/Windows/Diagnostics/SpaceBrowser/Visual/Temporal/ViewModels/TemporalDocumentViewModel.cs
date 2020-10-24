namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;

    public class TemporalDocumentViewModel : GraphDocumentViewModelBase, ITemporalDocumentViewModel
    {
        public TemporalDocumentViewModel(
            IFabricContext fabric,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu,
            IGraphContext graphContext)
            : base(fabric, buttons, contextMenu, graphContext)
        {
        }
    }
}
