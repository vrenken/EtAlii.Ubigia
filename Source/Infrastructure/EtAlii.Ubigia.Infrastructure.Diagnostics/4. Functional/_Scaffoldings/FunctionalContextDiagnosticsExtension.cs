// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class FunctionalContextDiagnosticsExtension : IExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public FunctionalContextDiagnosticsExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        /// <inheritdoc />
        public void Initialize(IRegisterOnlyContainer container)
        {
            // TODO: Refactor to "Infrastructure:Functional:Diagnostics"
            var options = _configurationRoot
                .GetSection("Infrastructure:Fabric:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new FunctionalDebuggingScaffolding(options),
                new FunctionalLoggingScaffolding(options),
                new FunctionalProfilingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
