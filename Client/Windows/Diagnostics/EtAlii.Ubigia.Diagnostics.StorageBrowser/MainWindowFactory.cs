namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class MainWindowFactory
    {
        public IMainWindow Create(IManagementConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var container = new Container();

            container.Register<IMainWindowViewModel, MainWindowViewModel>();

            container.Register<IMainWindow, MainWindow>();
            container.RegisterInitializer<IMainWindow>(window => window.DataContext = container.GetInstance<IMainWindowViewModel>());

            container.Register<IManagementConnection>(() => connection);
            //container.RegisterInitializer<IManagementConnection>(connection => connection.Open(App.Current.Address, App.Current.Account, App.Current.Password));

            container.Register<IStoragesViewModel, StoragesViewModel>();
            container.Register< IAccountsViewModel , AccountsViewModel >();
            container.Register<IRolesViewModel, RolesViewModel>();
            container.Register<ISpacesViewModel, SpacesViewModel >();

            container.Register<ILogFactory, DisabledLogFactory>();
            container.Register<ILogger>(() => container.GetInstance<ILogFactory>().Create("EtAlii", "EtAlii.Ubigia.Client.Windows.Diagnostics"));
            
            return container.GetInstance<IMainWindow>();
        }
    }
}
