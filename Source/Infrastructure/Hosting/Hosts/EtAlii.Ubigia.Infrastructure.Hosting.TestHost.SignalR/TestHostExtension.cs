// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class TestHostExtension : IHostExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public TestHostExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new();
            configurationRoot.Bind("Infrastructure:Hosting:Diagnostics", _configuration);
        }

        public void Register(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new TestHostScaffolding(),
                new TestHostProfilingScaffolding(_configuration),
                new TestHostLoggingScaffolding(_configuration),
                new TestHostDebuggingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
