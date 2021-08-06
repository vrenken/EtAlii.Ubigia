// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    public class ComponentsProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;
        private readonly ILogger _logger = Log.ForContext<ComponentsProfilingScaffolding>();

        public ComponentsProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectProfiling) // profiling is enabled
            {
                _logger.Verbose("Injecting component profiling decorators");

                container.RegisterDecorator(typeof(IItemStorage), typeof(ProfilingItemStorage));
                container.RegisterDecorator(typeof(IComponentStorage), typeof(ProfilingComponentStorage));
            }
        }
    }
}
