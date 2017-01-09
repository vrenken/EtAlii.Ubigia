namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class TemporalDocumentViewModel : GraphDocumentViewModelBase, ITemporalDocumentViewModel
    {
        public TemporalDocumentViewModel(
            IFabricContext fabric,
            ICommandProcessor commandProcessor,
            IGraphConfiguration configuration,
            IGraphButtonsViewModel buttons,
            IGraphContextMenuViewModel contextMenu)
            : base(fabric, commandProcessor, configuration, buttons, contextMenu)
        {
        }
    }
}
