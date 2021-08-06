// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public TestHostProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectProfiling) // profiling is enabled
            {
                // Invoke all DI container registrations involved in profiling the AspNet test host.
            }
        }
    }
}
