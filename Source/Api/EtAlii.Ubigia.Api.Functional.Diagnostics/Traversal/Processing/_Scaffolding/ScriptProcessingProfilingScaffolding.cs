// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public ScriptProcessingProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options?.InjectProfiling ?? false) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(services => services.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                // Add registrations needed for profiling.
            }
        }
    }
}
