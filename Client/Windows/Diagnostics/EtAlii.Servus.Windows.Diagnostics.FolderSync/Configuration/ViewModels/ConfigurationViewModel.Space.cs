﻿namespace EtAlii.Servus.Diagnostics.FolderSync
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.SignalR;
    using EtAlii.xTechnology.Mvvm;
    using Fluent;

    internal partial class ConfigurationViewModel : BindableBase
    {
        public ICommand SelectSpaceCommand { get { return _selectSpaceCommand; } }
        private readonly ICommand _selectSpaceCommand;

        public IDataConnection Connection { get { return _connection; } }
        private readonly IDataConnection _connection;

        private bool CanSelectSpace(object obj)
        {
            return true;
        }

        private void OnSelectSpace(object obj)
        {
            var window = (RibbonWindow)obj;
            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(SignalRTransportProvider.Create())
                .UseDialog(window);
            var newConnection = new DataConnectionFactory().Create(connectionConfiguration);
            if (newConnection != null)
            {
                if (_connection.Storage.Address != newConnection.Storage.Address ||
                    _connection.Account.Name != newConnection.Account.Name ||
                    _connection.Account.Password != newConnection.Account.Password ||
                    _connection.Space.Name != newConnection.Space.Name)
                {
                    var dr = MessageBox.Show(window,
                        "The changes will not take effect until the next time the program is started.\r\n\r\nDo you want to restart the program now?",
                        "Program restart required", MessageBoxButton.YesNo, MessageBoxImage.Information,
                        MessageBoxResult.Yes);
                    if (dr == MessageBoxResult.Yes)
                    {
                        Process.Start(Application.ResourceAssembly.Location, "10");
                        Application.Current.Shutdown();
                    }
                }
            }
        }
    }
}