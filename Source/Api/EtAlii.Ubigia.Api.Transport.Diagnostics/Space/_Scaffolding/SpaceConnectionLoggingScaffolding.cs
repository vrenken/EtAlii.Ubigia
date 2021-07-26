// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public SpaceConnectionLoggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(ISpaceConnection), typeof(LoggingSpaceConnection));
                container.RegisterDecorator(typeof(ISpaceTransport), typeof(LoggingSpaceTransport));

                container.RegisterDecorator(typeof(IRootDataClient), typeof(LoggingRootDataClient));
                container.RegisterDecorator(typeof(IEntryDataClient), typeof(LoggingEntryDataClient));
            }
        }
    }
}
