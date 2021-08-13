// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LoggingTraversalContextExtension : IFunctionalExtension
    {
        private readonly IConfigurationRoot _configurationRoot;

        public LoggingTraversalContextExtension(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var options = _configurationRoot
                .GetSection("Api:Functional:Diagnostics")
                .Get<DiagnosticsOptions>();

            if (options.InjectLogging)
            {
                // Register all logging related DI mappings.
            }
        }
    }
}
