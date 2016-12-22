namespace EtAlii.Servus.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Windows.Management;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Servus.Api.Management.SignalR;
    using EtAlii.Servus.Api.Transport.SignalR;
    using EtAlii.xTechnology.Diagnostics;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public new MainWindow MainWindow { get { return base.MainWindow as MainWindow; } set { base.MainWindow = value; } }

        public App()
        {
            DispatcherUnhandledException += (sender, e) =>
            {
                //Logger.ReportUnhandledException(e.Exception); // Disabled because of performance loss.
                e.Handled = true;
            };
        }

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var address = e.Args.Length > 0 ? e.Args[0] : String.Empty;
            var account = e.Args.Length > 1 ? e.Args[1] : String.Empty;
            var password = e.Args.Length > 2 ? e.Args[2] : String.Empty;

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Servus.StorageBrowser");

            IManagementConnection connection;
            var factory = new ManagementConnectionFactory();
            if (String.IsNullOrWhiteSpace(address) ||
                String.IsNullOrWhiteSpace(account) ||
                String.IsNullOrWhiteSpace(password))
            {
                var configuration = new ManagementConnectionConfiguration()
                    .Use(SignalRStorageTransportProvider.Create())
                    .Use(diagnostics)
                    .UseDialog(ConnectionDialogOptions.ShowAlways, address, account, password);
                connection = factory.Create(configuration);
            }
            else
            {
                var configuration = new ManagementConnectionConfiguration()
                    .Use(SignalRStorageTransportProvider.Create())
                    .Use(address)
                    .Use(account, password)
                    .Use(diagnostics);
                connection = factory.Create(configuration);
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
                    MessageBox.Show("Connection failed", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
                    connection = null;
                }
            }
            if (connection != null)
            {
                MainWindow = new MainWindowFactory().Create(connection, diagnostics);
                MainWindow.Show();
            }
            else
            {
                this.Shutdown();
            }
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
        }
    }
}
