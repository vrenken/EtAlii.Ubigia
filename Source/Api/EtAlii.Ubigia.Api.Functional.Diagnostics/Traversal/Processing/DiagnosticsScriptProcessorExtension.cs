// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsScriptProcessorExtension : IExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public DiagnosticsScriptProcessorExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsOptions>();

            var scaffoldings = new IScaffolding[]
            {
                new ScriptProcessingDiagnosticsScaffolding(options),
                new ScriptProcessingLoggingScaffolding(options),
                new ScriptProcessingProfilingScaffolding(options),
                new ScriptProcessingDebuggingScaffolding(options),

                new ScriptExecutionPlannerLoggingScaffolding(options),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
