namespace EtAlii.Ubigia.Windows.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;

    internal class SignalRConnector : IConnector
    {
        public IManagementConnection Connect(ConnectionDialogWindow window, ConnectionDialogViewModel viewModel, out bool connectionSucceeded)
        {
            
            var passwordBox = window.PasswordBox;
            var password = passwordBox.Password;

            try
            {
                var address = new Uri(viewModel.Address, UriKind.Absolute);
                var connectionConfiguration = new ManagementConnectionConfiguration()
                    .Use(SignalRStorageTransportProvider.Create())
                    .Use(address)
                    .Use(viewModel.Account, password);

                var connection = new ManagementConnectionFactory().Create(connectionConfiguration);

                var task = Task.Run(async () =>
                {
                    await connection.Open().ConfigureAwait(false);
                });
                task.Wait();

                connectionSucceeded = true;
                return connection;
            }
            catch (Exception)
            {
                connectionSucceeded = false;
                return null;
            }
        }
    }
}
