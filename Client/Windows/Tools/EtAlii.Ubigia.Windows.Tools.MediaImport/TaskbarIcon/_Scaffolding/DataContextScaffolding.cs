namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional.Scripting;
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
            container.Register(() => _connection);

            
            container.Register(() =>
            {
                var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

                var configuration = new GraphSLScriptContextConfiguration()
                    .UseFunctionalGraphSLDiagnostics(diagnostics)
                    .Use(_connection)
                    .UseLogicalDiagnostics(diagnostics);
                return new GraphSLScriptContextFactory().Create(configuration);
            });
        }
    }
}