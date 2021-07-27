// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsHostExtension : IHostExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsHostExtension(IConfiguration configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Hosting:Diagnostics", _configuration);
        }

        public void Register(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new HostDebuggingScaffolding(_configuration),
                new HostLoggingScaffolding(_configuration),
                new HostProfilingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
