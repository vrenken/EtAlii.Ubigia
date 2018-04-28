namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.Diagnostics;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => Application.Current as App;

        public new IMainWindow MainWindow { get { return base.MainWindow as IMainWindow; } set { base.MainWindow = value as Window; } }

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
            var space = e.Args.Length > 3 ? e.Args[3] : String.Empty;

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.SpaceBrowser");

            IProfilingDataConnection connection;
            var factory = new DataConnectionFactory();
            if (String.IsNullOrWhiteSpace(address) ||
                String.IsNullOrWhiteSpace(account) ||
                String.IsNullOrWhiteSpace(password) ||
                String.IsNullOrWhiteSpace(space))
            {
                var connectionConfiguration = new DataConnectionConfiguration()
                    .Use(SignalRTransportProvider.Create())
                    .Use(diagnostics)
                    .UseDialog(ConnectionDialogOptions.ShowAlways, address, account, password, space);
                connection = factory.CreateForProfiling(connectionConfiguration);
            }
            else
            {
                var connectionConfiguration = new DataConnectionConfiguration()
                    .Use(SignalRTransportProvider.Create())
                    .Use(diagnostics)
                    .Use(new Uri(address, UriKind.Absolute))
                    .Use(account, space, password);
                connection = factory.CreateForProfiling(connectionConfiguration);
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
                Shutdown();
            }
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
        }
    }
}
