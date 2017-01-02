namespace EtAlii.Ubigia.Windows.Diagnostics.StorageBrowser
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class MainWindowFactory
    {
        public MainWindow Create(IManagementConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var container = new Container();
            container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.Name); };

            container.Register<MainWindowViewModel, MainWindowViewModel>(Lifestyle.Singleton);

            container.Register<MainWindow, MainWindow>(Lifestyle.Singleton);
            container.RegisterInitializer<MainWindow>(window => window.DataContext = container.GetInstance<MainWindowViewModel>());

            container.Register<IManagementConnection>(() => connection, Lifestyle.Singleton);
            //container.RegisterInitializer<IManagementConnection>(connection => connection.Open(App.Current.Address, App.Current.Account, App.Current.Password));

            container.Register<StoragesViewModel>(Lifestyle.Singleton);
            container.Register<AccountsViewModel>(Lifestyle.Singleton);
            container.Register<RolesViewModel>(Lifestyle.Singleton);
            container.Register<SpacesViewModel>(Lifestyle.Singleton);

            container.Register<ILogFactory, DisabledLogFactory>(Lifestyle.Singleton);
            container.Register<ILogger>(() => container.GetInstance<ILogFactory>().Create("EtAlii", "EtAlii.Ubigia.Client.Windows.Diagnostics"), Lifestyle.Singleton);
            
            return container.GetInstance<MainWindow>();
        }
    }
}
