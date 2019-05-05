// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;

    public static class GraphSLScriptContextFactoryCreateContextExtension
    {
        public static IGraphSLScriptContext Create(this GraphSLScriptContextFactory factory, IDataConnection connection, bool useCaching = true)
        {
            var configuration = new LogicalContextConfiguration()
                .UseCaching(useCaching)
                //.Use(_diagnostics)
                .UseTraversalCaching(useCaching)
                .Use(connection);
            var logicalContext = new LogicalContextFactory().Create(configuration);

            var dataContextConfiguration = new GraphSLScriptContextConfiguration()
                //.Use(_diagnostics)
                .Use(logicalContext);
            return factory.Create(dataContextConfiguration);
        }
    }
}