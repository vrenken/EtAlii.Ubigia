namespace EtAlii.Servus.Windows.Management
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Management.SignalR;
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
                    var configuration = new ManagementConnectionConfiguration()
                        .Use(SignalRStorageTransportProvider.Create())
                        .Use(Address)
                        .Use(Account, password);
                    var connection = new ManagementConnectionFactory().Create(configuration);

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
