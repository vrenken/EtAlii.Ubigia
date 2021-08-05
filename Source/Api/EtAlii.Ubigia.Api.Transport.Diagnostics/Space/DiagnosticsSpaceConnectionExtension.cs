// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsSpaceConnectionExtension : ISpaceConnectionExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Api:Transport:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            var scaffoldings = new IScaffolding[]
            {
                new SpaceConnectionLoggingScaffolding(configuration),
                new SpaceConnectionProfilingScaffolding(configuration),
                new SpaceConnectionDebuggingScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
