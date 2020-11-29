namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Threading;
    using System.Windows;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.Ubigia.Windows.Tools.MediaImport.Properties;
    using EtAlii.xTechnology.Diagnostics;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ITaskbarIconHost _host;

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE2NTUxQDMxMzgyZTMyMmUzMEtFNXoxRkpIeUNoK1FDa3JobnFJVVlpcTVzRzczeDdyKzVDb3hMTThKejA9");

            var startupDelay = e.Args.Length > 0 ? int.Parse(e.Args[0]) * 1000 : 0;
            Thread.Sleep(startupDelay);

            var diagnostics = DiagnosticsConfiguration.Default;
            var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransport(SignalRTransportProvider.Create())
                .UseTransportDiagnostics(diagnostics)
                .UseDialog(ConnectionDialogOptions.ShowOnlyWhenNeeded);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);
            if (connection != null)
            {
                var folderSyncConfigurations = CreateFolderSyncConfigurations();

                _host = new TaskbarIconHostFactory().Create(diagnostics, connection, folderSyncConfigurations);
                _host.Start();


            }
            else
            {
                Shutdown();
            }
        }

        // private IDiagnosticsConfiguration CreateDiagnosticsConfiguration()
        // [
        //     var diagnostics = new DiagnosticsFactory().Create(false, false, false,
        //         () => new DisabledLogFactory(),
        //         () => new DisabledProfilerFactory(),
        //         (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Windows.Tools.MediaImport"),
        //         (factory) => factory.Create("EtAlii", "EtAlii.Ubigia.Windows.Tools.MediaImport"))
        //     return diagnostics;
        // ]

        private IObservableFolderSyncConfigurationCollection CreateFolderSyncConfigurations()
        {
            var folderSyncConfigurations = new ObservableFolderSyncConfigurationCollection();

            foreach (var folder in Settings.Default.Folders)
            {
                var parts = folder.Split('|');
                var folderSyncConfiguration = new FolderSyncConfiguration(folderSyncConfigurations)
                {
                    LocalFolder = parts[0],
                    RemoteName = parts[1],
                };
                folderSyncConfigurations.Add(folderSyncConfiguration);
            }

            return folderSyncConfigurations;
        }
    }
}
