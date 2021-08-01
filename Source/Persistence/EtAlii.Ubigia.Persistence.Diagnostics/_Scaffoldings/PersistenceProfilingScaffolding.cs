// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    internal class PersistenceProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;
        private readonly ILogger _logger = Log.ForContext<PersistenceProfilingScaffolding>();

        internal PersistenceProfilingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectProfiling)
            {
                _logger.Verbose("Injecting persistence profiling decorators");

                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(() => container.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));
            }
        }
    }
}
