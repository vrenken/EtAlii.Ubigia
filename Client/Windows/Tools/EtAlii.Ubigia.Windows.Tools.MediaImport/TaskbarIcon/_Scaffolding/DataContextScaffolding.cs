namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.NET47;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

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
            container.Register<IDataConnection>(() => _connection);

            
            container.Register<IGraphSLScriptContext>(() =>
            {
                var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();
                var fabricContextConfiguration = new FabricContextConfiguration()
                    .Use(_connection);
                var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

                var logicalContextConfiguration = new LogicalContextConfiguration()
                    .Use(fabricContext)
                    .UseLogicalDiagnostics(diagnostics);
                var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

                var configuration = new GraphSLScriptContextConfiguration()
                    .UseFunctionalDiagnostics(diagnostics)
                    .Use(logicalContext)
                    .UseDotNet47();
                return new GraphSLScriptContextFactory().Create(configuration);
            });
        }
    }
}