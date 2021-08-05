// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsInfrastructureExtension : IInfrastructureExtension
    {
        /// <inheritdoc />
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Infrastructure:Fabric:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            var scaffoldings = new IScaffolding[]
            {
                new InfrastructureDebuggingScaffolding(configuration),
                new InfrastructureLoggingScaffolding(configuration),
                new InfrastructureProfilingScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
