// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public StorageConnectionLoggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IStorageConnection), typeof(LoggingStorageConnection));
                container.RegisterDecorator(typeof(IStorageTransport), typeof(LoggingStorageTransport));
            }
        }
    }
}
