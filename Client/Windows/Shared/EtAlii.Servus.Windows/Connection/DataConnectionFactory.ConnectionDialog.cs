namespace EtAlii.Servus.Api
{
    using System;
    using System.Windows;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Windows;

    public static class ConnectionDialogDataConnectionFactoryExtension
    {
        public static IDataConnection CreateUsingDialog<TTransport>(
            this DataConnectionFactory dataConnectionFactory,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways)
            where TTransport : ITransport, new()
        {
            return CreateUsingDialog<TTransport>(dataConnectionFactory, null, dialogOptions, null, null);
        }

        public static IDataConnection CreateUsingDialog<TTransport>(
            this DataConnectionFactory dataConnectionFactory, 
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
            where TTransport : ITransport, new()
        {
            return CreateUsingDialog<TTransport>(dataConnectionFactory, null, dialogOptions, null, null, defaultAddress, defaultAccount, defaultPassword, defaultSpace);
        }

        public static IDataConnection CreateUsingDialog<TTransport>(
            this DataConnectionFactory dataConnectionFactory,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            IDiagnosticsConfiguration diagnostics = null,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
            where TTransport : ITransport, new()
        {
            return CreateUsingDialog<TTransport>(dataConnectionFactory, null, dialogOptions, null, null, defaultAddress, defaultAccount, defaultPassword, defaultSpace);
        }

        public static IDataConnection CreateUsingDialog(
            this DataConnectionFactory dataConnectionFactory,
            ITransport transport,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            IDiagnosticsConfiguration diagnostics = null,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
        {
            return CreateUsingDialog(dataConnectionFactory, transport, new IDataConnectionExtension[0], null, dialogOptions, diagnostics, null, defaultAddress, defaultAccount, defaultPassword, defaultSpace);
        }

        public static IDataConnection CreateUsingDialog<TTransport>(
            this DataConnectionFactory dataConnectionFactory,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            IDiagnosticsConfiguration diagnostics = null,
            IInfrastructureClient client = null,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
            where TTransport : ITransport, new()
        {
            var transport = new TTransport();
            return CreateUsingDialog(dataConnectionFactory, transport, new IDataConnectionExtension[0], dialogOwner, dialogOptions, diagnostics, client, defaultAddress, defaultAccount, defaultPassword, defaultSpace);
        }

        public static IDataConnection CreateUsingDialog(
            this DataConnectionFactory dataConnectionFactory,
            ITransport transport,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            IDiagnosticsConfiguration diagnostics = null,
            IInfrastructureClient client = null,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "",
            string defaultSpace = "")
        {
            return CreateUsingDialog(dataConnectionFactory, transport, new IDataConnectionExtension[0], dialogOwner, dialogOptions, diagnostics, client, defaultAddress, defaultAccount, defaultPassword, defaultSpace);
        }

        public static IDataConnection CreateUsingDialog(
            this DataConnectionFactory dataConnectionFactory, 
            ITransport transport,
            IDataConnectionExtension[] extensions, 
            Window dialogOwner = null, 
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways, 
            IDiagnosticsConfiguration diagnostics = null,
            IInfrastructureClient client = null,
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
                connection = dataConnectionFactory.Create(transport, diagnostics, client);
                try
                {
                    connection.Open(viewModel.Address, viewModel.Account, window.PasswordBox.Password, viewModel.Space);
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
