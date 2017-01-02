namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class TemporalViewModel : GraphDocumentViewModelBase
    {
        public TemporalViewModel(
            IFabricContext fabric,
            ICommandProcessor commandProcessor,
            GraphConfiguration configuration,
            GraphButtonsViewModel buttons,
            GraphContextMenuViewModel contextMenu)
            : base(fabric, commandProcessor, configuration, buttons, contextMenu)
        {
        }
    }
}
