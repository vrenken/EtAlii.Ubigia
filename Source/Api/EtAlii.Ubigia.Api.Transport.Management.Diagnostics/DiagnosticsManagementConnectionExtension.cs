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
            var configurationRoot = container.GetInstance<IConfigurationRoot>();
            var options = configurationRoot
                .GetSection("Api:Transport:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new ManagementConnectionLoggingScaffolding(options),
                new ManagementConnectionProfilingScaffolding(options),
                new ManagementConnectionDebuggingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
