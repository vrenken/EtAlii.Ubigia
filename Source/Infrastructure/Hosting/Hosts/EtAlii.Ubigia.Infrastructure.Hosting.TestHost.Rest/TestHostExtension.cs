// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostExtension : IHostExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public TestHostExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TestHostScaffolding(),
                new TestHostProfilingScaffolding(_diagnostics),
                new TestHostLoggingScaffolding(_diagnostics),
                new TestHostDebuggingScaffolding(_diagnostics),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}