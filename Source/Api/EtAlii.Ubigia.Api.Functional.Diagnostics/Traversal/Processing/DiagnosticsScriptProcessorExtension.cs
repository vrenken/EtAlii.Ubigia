// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsScriptProcessorExtension : IScriptProcessorExtension
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        internal DiagnosticsScriptProcessorExtension(IConfigurationRoot configurationRoot)
        {
            _configuration = new DiagnosticsConfigurationSection();
            configurationRoot.Bind("Api:Functional:Diagnostics", _configuration);
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new ScriptProcessingLoggingScaffolding(_configuration),
                new ScriptProcessingProfilingScaffolding(_configuration),
                new ScriptProcessingDebuggingScaffolding(_configuration),

                new ScriptExecutionPlannerLoggingScaffolding(_configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
