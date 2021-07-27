// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public static class TraversalContextConfigurationDiagnosticsExtension
    {
        public static TTraversalContextConfiguration UseFunctionalTraversalDiagnostics<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration, IConfiguration configurationRoot, bool alsoUseForDeeperDiagnostics = true)
            where TTraversalContextConfiguration : FunctionalContextConfiguration
        {
            var extensions = new ITraversalContextExtension[]
            {
                new LoggingTraversalContextExtension(configurationRoot)
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseLogicalDiagnostics(configurationRoot);
            }

            return configuration;
        }
    }
}
