namespace EtAlii.Ubigia.Diagnostics.FolderSync
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using SimpleInjector;

    internal class DataContextScaffolding : IScaffolding
    {
        private readonly IDataConnection _connection;

        public DataContextScaffolding(IDataConnection connection)
        {
            _connection = connection;
        }

        public void Register(Container container)
        {
            // This should actually not be needed anymore because the datacontext should be the sole entry point for an application.
            // However, I have no good idea on how to redesign it that way. All other solutions have disadvantages as well.  
            container.Register<IDataConnection>(() => _connection, Lifestyle.Singleton);

            container.Register<IDataContext>(() =>
            {
                var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();
                var fabricContextConfiguration = new FabricContextConfiguration()
                    .Use(_connection);
                var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

                var logicalContextConfiguration = new LogicalContextConfiguration()
                    .Use(fabricContext)
                    .Use(diagnostics);
                var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

                var dataContextConfiguration = new DataContextConfiguration()
                                    .Use(diagnostics)
                                    .Use(logicalContext)
                                    .UseWin32();
                return new DataContextFactory().Create(dataContextConfiguration);
            }, Lifestyle.Singleton);
        }
    }
}