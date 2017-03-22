﻿namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionLoggingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateLogFactory());
            container.Register(() => diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));

            if (diagnostics.EnableLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(ISpaceConnection), typeof(LoggingSpaceConnection));
                container.RegisterDecorator(typeof(ISpaceTransport), typeof(LoggingSpaceTransport));

                container.RegisterDecorator(typeof(IRootDataClient), typeof(LoggingRootDataClient));
                container.RegisterDecorator(typeof(IEntryDataClient), typeof(LoggingEntryDataClient));
            }
        }
    }
}
