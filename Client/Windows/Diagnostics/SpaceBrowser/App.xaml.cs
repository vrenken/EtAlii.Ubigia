namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public new static App Current => Application.Current as App;

        public new IMainWindow MainWindow { get => base.MainWindow as IMainWindow; set => base.MainWindow = value as Window; }

        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTM5ODI2QDMxMzcyZTMyMmUzMGVWWXpJWXRhR040Nml1WFpTMFhnVHF5V0l3YnJaVVA0dkFuYU0wdnlzVWM9");
            
            if (!Debugger.IsAttached)
            {
                DispatcherUnhandledException += (sender, e) =>
                {
                    MessageBox.Show($"Unhandled exception: {Environment.NewLine} " +
                                    $"{Environment.NewLine}" +
                                    $"{e.Exception.Message}{Environment.NewLine}" +
                                    $"{Environment.NewLine}" +
                                    $"{e.Exception.StackTrace}", "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Logger.ReportUnhandledException(e.Exception); // Disabled because of performance loss.
                    //e.Handled = true
                };
            }
        }

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var address = e.Args.Length > 0 ? e.Args[0] : string.Empty;
            var account = e.Args.Length > 1 ? e.Args[1] : string.Empty;
            var password = e.Args.Length > 2 ? e.Args[2] : string.Empty;
            var space = e.Args.Length > 3 ? e.Args[3] : string.Empty;

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Ubigia.SpaceBrowser");

            IProfilingDataConnection connection;

            var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransportDiagnostics(diagnostics)
                .UseDataConnectionProfiling();
            
            if (string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(account) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(space))
            {
                connectionConfiguration = connectionConfiguration.UseDialog(ConnectionDialogOptions.ShowAlways, address, account, password, space);
                connection = (IProfilingDataConnection)new DataConnectionFactory().Create(connectionConfiguration);
            }
            else
            {
                connectionConfiguration = connectionConfiguration
                    .Use(new Uri(address, UriKind.Absolute))
                    .Use(account, space, password);
                connection = (IProfilingDataConnection)new DataConnectionFactory().Create(connectionConfiguration);
                try
                {
                    connection.Open().Wait();
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
            // Handle an application exit gracefully.
        }
    }
}
