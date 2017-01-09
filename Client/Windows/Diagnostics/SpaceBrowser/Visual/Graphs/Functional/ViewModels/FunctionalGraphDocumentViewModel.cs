namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Workflow;

    public class FunctionalGraphDocumentViewModel : GraphDocumentViewModelBase, IFunctionalGraphDocumentViewModel
    {
        public FunctionalGraphDocumentViewModel(
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
