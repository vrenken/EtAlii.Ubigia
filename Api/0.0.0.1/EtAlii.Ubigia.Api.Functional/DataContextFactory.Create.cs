// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public static class DataContextFactoryCreateDataContextExtension
    {
        public static IDataContext Create(this DataContextFactory factory, IDataConnection connection, bool useCaching = true)
        {
            var fabricContextConfiguration = new FabricContextConfiguration()
                .UseTraversalCaching(useCaching)
                .Use(connection);
            var fabricContext = new FabricContextFactory().Create(fabricContextConfiguration);

            var logicalContextConfiguration = new LogicalContextConfiguration()
                .UseCaching(useCaching)
                //.Use(_diagnostics)
                .Use(fabricContext);
            var logicalContext = new LogicalContextFactory().Create(logicalContextConfiguration);

            var dataContextConfiguration = new DataContextConfiguration()
                //.Use(_diagnostics)
                .Use(logicalContext);
            return factory.Create(dataContextConfiguration);
        }
    }
}