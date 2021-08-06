// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    internal class ScriptParserLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public ScriptParserLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            if (_options.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptParser), typeof(LoggingScriptParser));
                container.RegisterDecorator(typeof(IPathParser), typeof(LoggingPathParser));

                container.Register<IContextCorrelator, ContextCorrelator>();
            }
        }
    }
}
