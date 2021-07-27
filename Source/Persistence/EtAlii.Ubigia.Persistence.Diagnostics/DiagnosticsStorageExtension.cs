// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsStorageExtension : IStorageExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsStorageExtension(IConfiguration configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Persistence:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new PersistenceProfilingScaffolding(_configuration),
                new BlobsLoggingScaffolding(_configuration),
                new ComponentsProfilingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
