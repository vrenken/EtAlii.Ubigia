// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public TestHostProfilingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectProfiling) // profiling is enabled
            {
                // Invoke all DI container registrations involved in profiling the AspNet test host.
            }
        }
    }
}
