namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Windows;

    public static class DataConnectionConfigurationExtension
    {
        public static IDataConnectionConfiguration UseDialog(
            this IDataConnectionConfiguration configuration,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
        {
            configuration.Use(() => CreateUsingDialog(configuration, null, dialogOptions, defaultAddress, defaultAccount, defaultPassword, defaultSpace));

            return configuration;
        }

        public static IDataConnectionConfiguration UseDialog(
            this IDataConnectionConfiguration configuration,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
        {
            return configuration.Use(() => CreateUsingDialog(configuration, dialogOwner, dialogOptions, defaultAddress, defaultAccount, defaultPassword, defaultSpace));
        }

        private static IDataConnection CreateUsingDialog(
            IDataConnectionConfiguration configuration,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
        {
            var previousShutdownMode = Application.Current.ShutdownMode;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            bool? tryReturnConnection = true;
            var window = new ConnectionDialogWindow
            {
                Owner = dialogOwner,
                WindowStartupLocation = dialogOwner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner,
            };
            var viewModel = new ConnectionDialogViewModel(window, defaultAddress, defaultAccount, defaultPassword, defaultSpace);
            if (!viewModel.IsTested || dialogOptions == ConnectionDialogOptions.ShowAlways)
            {
                window.DataContext = viewModel;
                window.PasswordBox.PasswordChanged += viewModel.HandlePasswordChanged;
                tryReturnConnection = window.ShowDialog();
                window.PasswordBox.PasswordChanged -= viewModel.HandlePasswordChanged;
            }

            IDataConnection connection = null;
            if (tryReturnConnection == true)
            {
				var address = new Uri(viewModel.Address, UriKind.Absolute);

				// We need to provide a clean configuration. else the factoryextension func will be called over and over. 
				configuration = new DataConnectionConfiguration()
                    .Use(configuration.TransportProvider)
                    //.Use(configuration.Diagnostics)
                    .Use(address)
                    .Use(viewModel.Account, viewModel.Space, window.PasswordBox.Password)
                    .Use(configuration.Extensions);

                connection = new DataConnectionFactory().Create(configuration);
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
                    MessageBox.Show(window, "Connection failed", "Connection", MessageBoxButton.OK,
                        MessageBoxImage.Error);
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

