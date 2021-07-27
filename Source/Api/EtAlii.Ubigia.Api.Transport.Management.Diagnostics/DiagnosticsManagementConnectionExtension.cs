// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsManagementConnectionExtension : IManagementConnectionExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsManagementConnectionExtension(IConfiguration configurationRoot)
        {
            _configuration = new();
            configurationRoot.Bind("Api:Transport:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionLoggingScaffolding(_configuration),
                new ManagementConnectionProfilingScaffolding(_configuration),
                new ManagementConnectionDebuggingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
