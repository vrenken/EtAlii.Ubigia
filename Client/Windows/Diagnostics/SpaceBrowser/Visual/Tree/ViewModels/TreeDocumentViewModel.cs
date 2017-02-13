namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;

    public class TreeDocumentViewModel : GraphDocumentViewModelBase, ITreeDocumentViewModel
    {
        public TreeDocumentViewModel(
            IFabricContext fabric,
            IGraphContext graphContext,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu)
            : base(fabric, buttons, contextMenu, graphContext)
        {
        }
    }
}
