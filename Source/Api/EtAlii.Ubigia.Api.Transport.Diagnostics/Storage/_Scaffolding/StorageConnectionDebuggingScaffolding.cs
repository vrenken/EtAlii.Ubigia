// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public StorageConnectionDebuggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectDebugging) // logging is enabled
            {
                // Invoke all DI container registrations involved in debugging the StorageConnection.
            }
        }
    }
}
