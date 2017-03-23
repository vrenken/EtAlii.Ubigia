namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.xTechnology.Mvvm;
    using Fluent;

    internal partial class ConfigurationViewModel : BindableBase
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
                .Use(SignalRTransportProvider.Create())
                .UseDialog(window);
            var newConnection = new DataConnectionFactory().Create(connectionConfiguration);
            if (newConnection != null)
            {
                if (Connection.Storage.Address != newConnection.Storage.Address ||
                    Connection.Account.Name != newConnection.Account.Name ||
                    Connection.Account.Password != newConnection.Account.Password ||
                    Connection.Space.Name != newConnection.Space.Name)
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