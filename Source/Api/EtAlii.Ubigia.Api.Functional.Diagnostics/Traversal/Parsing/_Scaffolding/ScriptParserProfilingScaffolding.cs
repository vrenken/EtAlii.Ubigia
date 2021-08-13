// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserProfilingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public ScriptParserProfilingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectProfiling) // profiling is enabled
            {
                container.Register<IProfilerFactory>(() => new DisabledProfilerFactory());
                container.Register(c => c.GetInstance<IProfilerFactory>().Create("EtAlii", "EtAlii.Ubigia"));

                // Add registrations needed for profiling.
            }

        }
    }
}
