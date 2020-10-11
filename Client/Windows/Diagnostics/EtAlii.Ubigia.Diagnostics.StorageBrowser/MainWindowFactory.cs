namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class MainWindowFactory
    {
        public IMainWindow Create(IManagementConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var container = new Container();

            container.Register<IMainWindowViewModel, MainWindowViewModel>();

            container.Register<IMainWindow, MainWindow>();
            container.RegisterInitializer<IMainWindow>(window => window.DataContext = container.GetInstance<IMainWindowViewModel>());

            container.Register(() => connection);
            //container.RegisterInitializer<IManagementConnection>(connection => connection.Open(App.Current.Address, App.Current.Account, App.Current.Password))

            container.Register<IStoragesViewModel, StoragesViewModel>();
            container.Register< IAccountsViewModel , AccountsViewModel >();
            container.Register<IRolesViewModel, RolesViewModel>();
            container.Register<ISpacesViewModel, SpacesViewModel >();
            
            return container.GetInstance<IMainWindow>();
        }
    }
}
