// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsDataConnectionExtension : IDataConnectionExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public DiagnosticsDataConnectionExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Transport:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new DataConnectionLoggingScaffolding(options),
                new DataConnectionProfilingScaffolding(options),
                new DataConnectionDebuggingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
