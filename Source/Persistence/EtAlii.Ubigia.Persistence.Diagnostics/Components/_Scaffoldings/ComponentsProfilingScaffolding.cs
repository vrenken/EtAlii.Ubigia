// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class ComponentsProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public ComponentsProfilingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IItemStorage), typeof(ProfilingItemStorage));
                container.RegisterDecorator(typeof(IComponentStorage), typeof(ProfilingComponentStorage));
            }
        }
    }
}
