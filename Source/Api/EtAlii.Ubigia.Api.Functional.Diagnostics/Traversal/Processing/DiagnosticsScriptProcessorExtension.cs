// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsScriptProcessorExtension : IScriptProcessorExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            var scaffoldings = new IScaffolding[]
            {
                new ScriptProcessingLoggingScaffolding(configuration),
                new ScriptProcessingProfilingScaffolding(configuration),
                new ScriptProcessingDebuggingScaffolding(configuration),

                new ScriptExecutionPlannerLoggingScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
