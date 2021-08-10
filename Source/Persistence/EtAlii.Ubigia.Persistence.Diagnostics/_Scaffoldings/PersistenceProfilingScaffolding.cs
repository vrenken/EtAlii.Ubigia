// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    internal class PersistenceProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;
        private readonly ILogger _logger = Log.ForContext<PersistenceProfilingScaffolding>();

        internal PersistenceProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectProfiling)
            {
                _logger.Verbose("Injecting persistence profiling decorators");

                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(services => services.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));
            }
        }
    }
}
