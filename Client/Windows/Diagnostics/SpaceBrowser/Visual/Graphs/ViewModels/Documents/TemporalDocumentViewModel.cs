﻿namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api.Fabric;
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
