namespace EtAlii.Ubigia.Windows
{
    using EtAlii.Ubigia.Api.Transport;

    internal interface IConnector
    {
        IDataConnection Connect(
            ConnectionDialogWindow window,
            ConnectionDialogViewModel viewModel,
            out bool connectionSucceeded,
            IDataConnectionExtension[] configurationExtensions = null);
    }
}