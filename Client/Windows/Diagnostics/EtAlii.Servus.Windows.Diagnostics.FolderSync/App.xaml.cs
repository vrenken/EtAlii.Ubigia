﻿namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.SignalR;
    using EtAlii.Servus.Diagnostics.FolderSync.Properties;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIconHost _host;

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            var startupDelay = e.Args.Length > 0 ? Int32.Parse(e.Args[0]) * 1000 : 0;
            Thread.Sleep(startupDelay);

            var diagnostics = CreateDiagnosticsConfiguration();
            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(SignalRTransportProvider.Create())
                .Use(diagnostics)
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
                this.Shutdown();
            }
        }

        private IDiagnosticsConfiguration CreateDiagnosticsConfiguration()
        {
            var diagnostics = new DiagnosticsFactory().Create(false, false, false,
                () => new DisabledLogFactory(),
                () => new DisabledProfilerFactory(),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.FolderSync"),
                (factory) => factory.Create("EtAlii", "EtAlii.Servus.FolderSync"));
            return diagnostics;
        }

        private ObservableCollection<FolderSyncConfiguration> CreateFolderSyncConfigurations()
        {
            var folderSyncConfigurations = new ObservableCollection<FolderSyncConfiguration>();

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
