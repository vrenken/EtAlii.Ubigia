namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using Fluent;

    internal partial class ConfigurationViewModel
    {
        public ICommand SelectSpaceCommand { get; }

        public IDataConnection Connection { get; }

        private bool CanSelectSpace(object obj)
        {
            return true;
        }

        private void OnSelectSpace(object obj)
        {
            var window = (RibbonWindow)obj;
            var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransport(SignalRTransportProvider.Create())
                .UseDialog(window);
            var newConnection = new DataConnectionFactory().Create(connectionConfiguration);
            
            if (newConnection == null || 
                (Connection.Storage.Address == newConnection.Storage.Address &&
                 Connection.Account.Name == newConnection.Account.Name &&
                 Connection.Account.Password == newConnection.Account.Password &&
                 Connection.Space.Name == newConnection.Space.Name)) return;
            
            var dr = MessageBox.Show(window,
                "The changes will not take effect until the next time the program is started.\r\n\r\nDo you want to restart the program now?",
                "Program restart required", MessageBoxButton.YesNo, MessageBoxImage.Information,
                MessageBoxResult.Yes);
            
            if (dr != MessageBoxResult.Yes) return;
            Process.Start(Application.ResourceAssembly.Location, "10");
            Application.Current.Shutdown();
        }
    }
}