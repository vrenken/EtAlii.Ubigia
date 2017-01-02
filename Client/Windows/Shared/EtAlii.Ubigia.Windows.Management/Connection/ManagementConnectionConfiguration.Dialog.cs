namespace EtAlii.Ubigia.Windows.Management
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Management;

    public static class ManagementConnectionConfigurationExtension
    {
        public static IManagementConnectionConfiguration UseDialog(
            this IManagementConnectionConfiguration configuration,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "")
        {
            configuration.Use(() => CreateUsingDialog(configuration, null, dialogOptions, defaultAddress, defaultAccount, defaultPassword));

            return configuration;
        }

        public static IManagementConnectionConfiguration UseDialog(
            this IManagementConnectionConfiguration configuration,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "")
        {
            return configuration.Use(() => CreateUsingDialog(configuration, dialogOwner, dialogOptions, defaultAddress, defaultAccount, defaultPassword));
        }

        private static IManagementConnection CreateUsingDialog(
            IManagementConnectionConfiguration configuration,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "")
        {
            var previousShutdownMode = Application.Current.ShutdownMode;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            bool? tryReturnConnection = true;
            var window = new ConnectionDialogWindow
            {
                Owner = dialogOwner,
                WindowStartupLocation = dialogOwner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner,
            };
            var viewModel = new ConnectionDialogViewModel(window, defaultAddress, defaultAccount, defaultPassword);
            if (!viewModel.IsTested || dialogOptions == ConnectionDialogOptions.ShowAlways)
            {
                window.DataContext = viewModel;
                window.PasswordBox.PasswordChanged += viewModel.HandlePasswordChanged;
                tryReturnConnection = window.ShowDialog();
                window.PasswordBox.PasswordChanged -= viewModel.HandlePasswordChanged;
            }

            IManagementConnection connection = null;
            if (tryReturnConnection == true)
            {
                configuration = new ManagementConnectionConfiguration()
                    .Use(configuration.TransportProvider)
                    .Use(configuration.Extensions)
                    .Use(viewModel.Address)
                    .Use(viewModel.Account, window.PasswordBox.Password);

                connection = new ManagementConnectionFactory().Create(configuration);
                try
                {
                    var task = Task.Run(async () =>
                    {
                        await connection.Open();
                    });
                    task.Wait();
                }
                catch (Exception)
                {
                    MessageBox.Show(window, "Connection failed", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                    connection = null;
                }
            }

            if (Application.Current.MainWindow is ConnectionDialogWindow)
            {
                Application.Current.MainWindow = null;
            }
            window.Close();

            Application.Current.ShutdownMode = previousShutdownMode;

            return connection;
        }
    }
}

