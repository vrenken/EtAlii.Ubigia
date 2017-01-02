namespace EtAlii.Servus.Windows.Management
{
    using System;
    using System.Windows;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;

    public static class ManagementConnectionFactoryExtension
    {
        public static IManagementConnection CreateUsingDialog<TTransport>(
            this ManagementConnectionFactory managementConnectionFactory,
            IManagementConnectionConfiguration configuration,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways)
            where TTransport : ITransport, new()
        {
            return CreateUsingDialog<TTransport>(managementConnectionFactory, configuration, null, dialogOptions, null, null);
        }

        //public static IManagementConnection CreateUsingDialog<TTransport>(
        //    this ManagementConnectionFactory managementConnectionFactory,
        //    IManagementConnectionConfiguration configuration,
        //    ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
        //    string defaultAddress = "",
        //    string defaultAccount = "",
        //    string defaultPassword = "")
        //    where TTransport : ITransport, new()
        //{
        //    return CreateUsingDialog<TTransport>(managementConnectionFactory, configuration, dialogOptions, defaultAddress, defaultAccount, defaultPassword);
        //}

        public static IManagementConnection CreateUsingDialog(
            this ManagementConnectionFactory managementConnectionFactory,
            IManagementConnectionConfiguration configuration,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "")
        {
            return CreateUsingDialog(managementConnectionFactory, configuration, null, dialogOptions, defaultAddress, defaultAccount, defaultPassword);
        }

        public static IManagementConnection CreateUsingDialog<TTransport>(
            this ManagementConnectionFactory managementConnectionFactory,
            IManagementConnectionConfiguration configuration,
            Window dialogOwner = null,
            ConnectionDialogOptions dialogOptions = ConnectionDialogOptions.ShowAlways,
            string defaultAddress = "",
            string defaultAccount = "",
            string defaultPassword = "")
            where TTransport : ITransport, new()
        {
            configuration.Use<TTransport>();
            return CreateUsingDialog(managementConnectionFactory, configuration, dialogOwner, dialogOptions, defaultAddress, defaultAccount, defaultPassword);
        }

        public static IManagementConnection CreateUsingDialog(
            this ManagementConnectionFactory managementConnectionFactory,
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
                connection = managementConnectionFactory.Create(configuration);
                try
                {
                    connection.Open(viewModel.Address, viewModel.Account, window.PasswordBox.Password);
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
