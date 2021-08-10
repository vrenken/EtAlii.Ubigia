// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public DiagnosticsInfrastructureExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        /// <inheritdoc />
        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Infrastructure:Fabric:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new InfrastructureDebuggingScaffolding(options),
                new InfrastructureLoggingScaffolding(options),
                new InfrastructureProfilingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
