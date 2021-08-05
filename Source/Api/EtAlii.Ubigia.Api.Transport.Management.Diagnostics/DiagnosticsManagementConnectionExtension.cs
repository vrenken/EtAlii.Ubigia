// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsManagementConnectionExtension : IManagementConnectionExtension
    {
        /// <inheritdoc />
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Api:Transport:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionLoggingScaffolding(configuration),
                new ManagementConnectionProfilingScaffolding(configuration),
                new ManagementConnectionDebuggingScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
