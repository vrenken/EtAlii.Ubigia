namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;

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
                var connectionSucceeded = false;
                switch (viewModel.Transport)
                {
                    case TransportType.SignalR:
                        connection = new SignalRConnector().Connect(window, viewModel, out connectionSucceeded, configuration.Extensions);
                        break;
                    case TransportType.Grpc:
                        connection = new GrpcConnector().Connect(window, viewModel, out connectionSucceeded, configuration.Extensions);
                        break;
                    case TransportType.WebApi:
                        connection = new WebApiConnector().Connect(window, viewModel, out connectionSucceeded, configuration.Extensions);
                        break;
                }
                if(!connectionSucceeded)
                {
                    MessageBox.Show(window, $"{viewModel.Transport} Connection failed", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
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

