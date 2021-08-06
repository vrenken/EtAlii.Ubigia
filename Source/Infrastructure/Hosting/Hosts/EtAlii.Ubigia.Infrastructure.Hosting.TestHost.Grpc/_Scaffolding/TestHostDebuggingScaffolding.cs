// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public TestHostDebuggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectDebugging) // debugging is enabled
            {
                // Invoke all DI container registrations involved in debugging the Grpc test host.
            }
        }
    }
}
