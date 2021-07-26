// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class FabricContextLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public FabricContextLoggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectLogging) // logging is enabled.
            {
                // Fabric.
                container.RegisterDecorator(typeof(IEntryGetter), typeof(LoggingEntryGetterDecorator));
                container.RegisterDecorator(typeof(IEntryStorer), typeof(LoggingEntryStorerDecorator));
                container.RegisterDecorator(typeof(IEntryUpdater), typeof(LoggingEntryUpdaterDecorator));
            }
        }
    }
}
