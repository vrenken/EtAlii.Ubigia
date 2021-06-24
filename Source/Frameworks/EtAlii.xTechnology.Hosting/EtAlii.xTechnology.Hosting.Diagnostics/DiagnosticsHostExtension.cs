// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsHostExtension : IHostExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsHostExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new HostDiagnosticsScaffolding(_diagnostics),
                new HostDebuggingScaffolding(_diagnostics),
                new HostLoggingScaffolding(_diagnostics),
                new HostProfilingScaffolding(_diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}