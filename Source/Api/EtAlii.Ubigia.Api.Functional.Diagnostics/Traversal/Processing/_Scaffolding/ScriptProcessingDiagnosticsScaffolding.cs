// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class ScriptProcessingDiagnosticsScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public ScriptProcessingDiagnosticsScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectDebugging ||
                _options.InjectLogging ||
                _options.InjectProfiling)
            {
                container.Register<IContextCorrelator, ContextCorrelator>();
            }
        }
    }
}
