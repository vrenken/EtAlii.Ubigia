namespace EtAlii.Servus.Api.Diagnostics
{
    using System;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;

    public static class DataContextFactoryCreateDataContextWithDiagnosticsExtension
    {
        public static IDataContext Create(this DataContextFactory factory, IDataConnection connection, IDiagnosticsConfiguration diagnostics)
        {
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .Use(diagnostics)
                .Use(fabricContext);
            var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

            var dataContextConfiguration = new DataContextConfiguration()
                .Use(diagnostics)
                .Use(logicalContext);
            return factory.Create(dataContextConfiguration);
        }
    }
}