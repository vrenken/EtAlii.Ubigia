// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScriptParserExtension : IScriptParserExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsScriptParserExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            var scaffoldings = new IScaffolding[]
            {
                new ScriptParserLoggingScaffolding(),
                new ScriptParserProfilingScaffolding(),
                new ScriptParserDebuggingScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
