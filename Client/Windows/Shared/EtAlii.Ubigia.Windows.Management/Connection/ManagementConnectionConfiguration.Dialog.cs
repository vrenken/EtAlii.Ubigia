namespace EtAlii.Ubigia.Windows.Management
{
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport.Management;

    public static class ManagementConnectionConfigurationExtension
    {
        public static IManagementConnectionConfiguration UseDialog(
            this IManagementConnectionConfiguration configuration,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "")
        {
            configuration.Use(() => CreateUsingDialog(null, dialogOptions, defaultAddress, defaultAccount, defaultPassword)); // configuration, 

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
            return configuration.Use(() => CreateUsingDialog(dialogOwner, dialogOptions, defaultAddress, defaultAccount, defaultPassword)); // configuration, 
        }

        private static IManagementConnection CreateUsingDialog(
            //IManagementConnectionConfiguration configuration,
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
                var connectionSucceeded = false;
                switch (viewModel.Transport)
                {
                    case TransportType.SignalR:
                        connection = new SignalRConnector().Connect(window, viewModel, out connectionSucceeded);
                        break;
                    case TransportType.Grpc:
                        connection = new GrpcConnector().Connect(window, viewModel, out connectionSucceeded);
                        break;
                    case TransportType.WebApi:
                        connection = new WebApiConnector().Connect(window, viewModel, out connectionSucceeded);
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

