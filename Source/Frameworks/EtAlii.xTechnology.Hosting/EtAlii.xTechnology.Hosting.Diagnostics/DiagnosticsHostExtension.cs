// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsHostExtension : IHostExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public DiagnosticsHostExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Host:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new HostDebuggingScaffolding(options),
                new HostLoggingScaffolding(options),
                new HostProfilingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
