﻿namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Diagnostics.Profiling;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.SignalR;
    using EtAlii.xTechnology.Diagnostics;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current { get { return Application.Current as App; } }

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
            var space = e.Args.Length > 3 ? e.Args[3] : String.Empty;

            var diagnostics = new DiagnosticsFactory().CreateDisabled("EtAlii", "EtAlii.Servus.SpaceBrowser");

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
                    .Use(address)
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
                this.Shutdown();
            }
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
        }
    }
}
