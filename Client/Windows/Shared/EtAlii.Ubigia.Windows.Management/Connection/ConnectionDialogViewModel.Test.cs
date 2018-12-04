namespace EtAlii.Ubigia.Windows.Management
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;

    internal partial class ConnectionDialogViewModel
    {
        private bool CanTest(object parameter)
        {
            var result = false;

            if (parameter is ConnectionDialogWindow window)
            {
                var passwordBox = window.PasswordBox;
                result = !String.IsNullOrEmpty(passwordBox.Password) &&
                         !String.IsNullOrEmpty(Account) &&
                         !String.IsNullOrEmpty(Address);
            }
            return result;
        }

        private void Test(object parameter)
        {
            if (parameter is ConnectionDialogWindow window)
            {
                var connectionSucceeded = false;
                switch (Transport)
                {
                    case TransportType.SignalR:
                        new SignalRConnector().Connect(window, this, out connectionSucceeded);
                        break;
                    case TransportType.Grpc:
                        new GrpcConnector().Connect(window, this, out connectionSucceeded);
                        break;
                    case TransportType.WebApi:
                        new WebApiConnector().Connect(window, this, out connectionSucceeded);
                        break;
                }

                var message = connectionSucceeded
                    ? $"{Transport} connection succeeded"
                    : $"{Transport} connection failed";
                var icon = connectionSucceeded
                    ? MessageBoxImage.None
                    : MessageBoxImage.Error;
                MessageBox.Show(window, message, "Connection test", MessageBoxButton.OK, icon);

                IsTested = connectionSucceeded;
            }
        }
    }
}
