namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphSLScriptContextFactoryCreateDataContextWithDiagnosticsExtension
    {
        public static IGraphSLScriptContext Create(this GraphSLScriptContextFactory factory, IDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(diagnostics)
                .Use(connection);
            
            var logicalContext = new LogicalContextFactory().Create(configuration);

            var dataContextConfiguration = new GraphSLScriptContextConfiguration()
                .UseFunctionalDiagnostics(diagnostics)
                .Use(logicalContext);
            return factory.Create(dataContextConfiguration);
        }
    }
}