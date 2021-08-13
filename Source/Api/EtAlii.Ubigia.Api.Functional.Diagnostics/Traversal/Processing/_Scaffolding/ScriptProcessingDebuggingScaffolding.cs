// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingDebuggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public ScriptProcessingDebuggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectDebugging) // debugging is enabled
            {
                // Add registrations needed for debugging.
            }
        }
    }
}
