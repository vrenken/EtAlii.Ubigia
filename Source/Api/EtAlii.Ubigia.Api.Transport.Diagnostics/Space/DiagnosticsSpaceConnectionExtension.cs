// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsSpaceConnectionExtension : ISpaceConnectionExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsSpaceConnectionExtension(IConfiguration configurationRoot)
        {
            _configuration = new();
            configurationRoot.Bind("Api:Transport:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SpaceConnectionLoggingScaffolding(_configuration),
                new SpaceConnectionProfilingScaffolding(_configuration),
                new SpaceConnectionDebuggingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
