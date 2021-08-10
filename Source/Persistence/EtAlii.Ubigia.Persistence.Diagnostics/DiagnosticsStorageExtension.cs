// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsStorageExtension : IExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public DiagnosticsStorageExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Persistence:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new PersistenceProfilingScaffolding(options),
                new BlobsLoggingScaffolding(options),
                new ComponentsProfilingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
