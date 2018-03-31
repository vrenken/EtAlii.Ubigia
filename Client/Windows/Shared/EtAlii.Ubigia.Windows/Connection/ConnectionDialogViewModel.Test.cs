namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.Mvvm;

    internal partial class ConnectionDialogViewModel : BindableBase
    {
        private bool CanTest(object parameter)
        {
            var result = false;

            var window = parameter as ConnectionDialogWindow;
            if (window != null)
            {
                var passwordBox = window.PasswordBox;
                result = !String.IsNullOrEmpty(passwordBox.Password) &&
                         !String.IsNullOrEmpty(Account) &&
                         !String.IsNullOrEmpty(Space) &&
                         !String.IsNullOrEmpty(Address);
            }
            return result;
        }

        private void Test(object parameter)
        {
            var window = parameter as ConnectionDialogWindow;
            if (window != null)
            {
                var passwordBox = window.PasswordBox;
                var password = passwordBox.Password;

                try
                {
	                var address = new Uri(Address, UriKind.Absolute);
                    var connectionConfiguration = new DataConnectionConfiguration()
                        .Use(SignalRTransportProvider.Create())
                        .Use(address)
                        .Use(Account, Space, password);

                    var connection = new DataConnectionFactory().Create(connectionConfiguration);

                    var task = Task.Run(async () =>
                    {
                        await connection.Open();
                    });
                    task.Wait();

                    MessageBox.Show(window, "Connection succeeded", "Connection test", MessageBoxButton.OK, MessageBoxImage.None);
                    IsTested = true;
                }
                catch (Exception)
                {
                    MessageBox.Show(window, "Connection failed", "Connection test", MessageBoxButton.OK, MessageBoxImage.Error);
                    IsTested = false;
                }
            }
        }
    }
}
