// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsStorageConnectionExtension : IStorageConnectionExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;
        internal DiagnosticsStorageConnectionExtension(IConfiguration configurationRoot)
        {
            _configuration = new();
            configurationRoot.Bind("Api:Transport:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new StorageConnectionLoggingScaffolding(_configuration),
                new StorageConnectionProfilingScaffolding(_configuration),
                new StorageConnectionDebuggingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
