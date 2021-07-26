// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsDataConnectionExtension : IDataConnectionExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsDataConnectionExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new();
            configurationRoot.Bind("Api:Transport:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new DataConnectionLoggingScaffolding(_configuration),
                new DataConnectionProfilingScaffolding(_configuration),
                new DataConnectionDebuggingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
