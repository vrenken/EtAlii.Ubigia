// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public SpaceConnectionDebuggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectDebugging) // debugging is enabled
            {
                // Invoke all DI container registrations involved in debugging the SpaceConnection.
            }
        }
    }
}
