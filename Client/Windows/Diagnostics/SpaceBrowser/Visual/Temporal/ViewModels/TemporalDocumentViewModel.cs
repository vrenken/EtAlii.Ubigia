namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
