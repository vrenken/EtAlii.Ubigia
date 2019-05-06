namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphSLScriptContextFactoryCreateDataContextWithDiagnosticsExtension
    {
        public static IGraphSLScriptContext Create(this GraphSLScriptContextFactory factory, IDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var dataContextConfiguration = new GraphSLScriptContextConfiguration()
                .UseFunctionalGraphSLDiagnostics(diagnostics)
                .UseLogicalDiagnostics(diagnostics)
                .Use(connection);
            return factory.Create(dataContextConfiguration);
        }
    }
}