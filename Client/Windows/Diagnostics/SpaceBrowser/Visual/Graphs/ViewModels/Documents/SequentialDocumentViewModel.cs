namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class SequentialDocumentViewModel : GraphDocumentViewModelBase
    {
        public SequentialDocumentViewModel(
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
