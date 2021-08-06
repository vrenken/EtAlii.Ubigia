// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class HostDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public HostDebuggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectDebugging) // debugging is enabled
            {
                // Register for debugging required DI instances.
            }
        }
    }
}
