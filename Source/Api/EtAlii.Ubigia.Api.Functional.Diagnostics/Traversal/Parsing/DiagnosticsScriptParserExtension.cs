// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DiagnosticsScriptParserExtension : IScriptParserExtension
    {
        public void Initialize(Container container)
        {
            var configurationRoot = container.GetInstance<IConfiguration>();
            var configuration = configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsConfigurationSection>();

            var scaffoldings = new IScaffolding[]
            {
                new ScriptParserLoggingScaffolding(configuration),
                new ScriptParserProfilingScaffolding(configuration),
                new ScriptParserDebuggingScaffolding(configuration),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
