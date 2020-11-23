namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    internal class WebApiConnector : IConnector
    {
        public IDataConnection Connect(
            ConnectionDialogWindow window,
            ConnectionDialogViewModel viewModel,
            out bool connectionSucceeded,
            IDataConnectionExtension[] configurationExtensions = null)
        {
            configurationExtensions = configurationExtensions ?? Array.Empty<IDataConnectionExtension>();


            var passwordBox = window.PasswordBox;
            var password = passwordBox.Password;

            try
            {
                var address = new Uri(viewModel.Address, UriKind.Absolute);
                var connectionConfiguration = new DataConnectionConfiguration()
                    .UseTransport(WebApiTransportProvider.Create())
                    .Use(address)
                    .Use(viewModel.Account, viewModel.Space, password)
                    .Use(configurationExtensions);

                var connection = new DataConnectionFactory().Create(connectionConfiguration);

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
