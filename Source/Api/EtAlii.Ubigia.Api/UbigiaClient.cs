﻿namespace EtAlii.Ubigia.Api
{
    // using EtAlii.Ubigia.Api.Fabric;
    // using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    // using EtAlii.Ubigia.Api.Functional;
    // using EtAlii.Ubigia.Api.Functional.Antlr.Context;
    // using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;
    // using EtAlii.Ubigia.Api.Functional.Context;
    // using EtAlii.Ubigia.Api.Functional.Traversal;
    // using EtAlii.Ubigia.Api.Logical;
    // using EtAlii.Ubigia.Api.Logical.Diagnostics;
    // using EtAlii.Ubigia.Api.Transport;
    // using EtAlii.Ubigia.Api.Transport.Diagnostics;
    // using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class UbigiaClient
    {
        public static UbigiaClient Create()
        {
            var container = new Container();
            //
            // container.Register(() => diagnostics);
            //
            // container.Register<UbigiaClient>(() => new UbigiaClient());
            // RegisterContext(container, connection, diagnostics);
            //
            // container.Register<IGraphContextFactory, GraphContextFactory>();
            //
            var client = container.GetInstance<UbigiaClient>();

            return client;
        }
//
//         private static void RegisterContext(Container container, IProfilingDataConnection connection, IDiagnosticsConfiguration diagnostics)
//         {
//             // We start with the connection.
//             container.Register<IDataConnection>(() => connection);
//             container.Register(() => connection);
//
//             var configuration = new FunctionalContextConfiguration()
//                 .Use(connection)
//                 .UseFunctionalTraversalDiagnostics(diagnostics)
//                 .UseAntlrTraversalParser()
//                 //.Use(container.GetInstance<ISpaceBrowserFunctionHandlersProvider>())
//                 .UseGraphContextProfiling();
//
//             // Then the fabric context.
// #pragma warning disable CA2000 // The fabric context and logical context are consumed by the other context instances.
//             var fabricContext = new FabricContextFactory().CreateForProfiling(configuration);
//             container.Register<IFabricContext>(() => fabricContext);
//             container.Register(() => fabricContext);
//
//             var logicalContext = new LogicalContextFactory().CreateForProfiling(configuration);
//             container.Register<ILogicalContext>(() => logicalContext);
//             container.Register(() => logicalContext);
// #pragma warning restore CA2000
//
//             container.Register(() => new TraversalContextFactory().Create(configuration));
//             container.Register(() => (IProfilingTraversalContext)container.GetInstance<ITraversalContext>());
//
//             container.Register(() =>
//             {
//                 var queryContextConfiguration = new FunctionalContextConfiguration()
//                     .Use(configuration)
//                     .UseAntlrContextParser();
//                 return new GraphContextFactory().Create(queryContextConfiguration);
//             });
//             container.Register(() => (IProfilingGraphContext)container.GetInstance<IGraphContext>());
//         }
    }
}
