// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.Extensions.Configuration;

    public static class GraphContextConfigurationDiagnosticsExtension
    {
        public static TGraphContextConfiguration UseFunctionalGraphContextDiagnostics<TGraphContextConfiguration>(this TGraphContextConfiguration configuration, IConfiguration configurationRoot, bool alsoUseForDeeperDiagnostics = true)
            where TGraphContextConfiguration : FunctionalContextConfiguration
        {
            var extensions = new IGraphContextExtension[]
            {
                new LoggingGraphContextExtension(configurationRoot),
                new ProfilingGraphContextExtension(configurationRoot),
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFunctionalTraversalDiagnostics(configurationRoot);
            }

            return configuration;
        }
    }
}
