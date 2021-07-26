// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class HostProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public HostProfilingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(() => container.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                // Register for profiling required DI instances.
            }
        }
    }
}
