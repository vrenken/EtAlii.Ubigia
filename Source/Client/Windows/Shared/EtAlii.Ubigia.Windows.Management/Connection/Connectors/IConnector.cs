namespace EtAlii.Ubigia.Windows.Management
{
    using EtAlii.Ubigia.Api.Transport.Management;

    internal interface IConnector
    {
        IManagementConnection Connect(ConnectionDialogWindow window, ConnectionDialogViewModel viewModel, out bool connectionSucceeded);

    }
}