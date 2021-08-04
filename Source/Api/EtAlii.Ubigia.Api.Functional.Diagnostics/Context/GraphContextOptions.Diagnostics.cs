// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.Extensions.Configuration;

    public static class GraphContextOptionsDiagnosticsExtension
    {
        public static TGraphContextOptions UseFunctionalGraphContextDiagnostics<TGraphContextOptions>(this TGraphContextOptions options, IConfiguration configurationRoot, bool alsoUseForDeeperDiagnostics = true)
            where TGraphContextOptions : FunctionalContextOptions
        {
            var extensions = new IGraphContextExtension[]
            {
                new LoggingGraphContextExtension(configurationRoot),
                new ProfilingGraphContextExtension(configurationRoot),
            };

            options = options.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                options = options.UseFunctionalTraversalDiagnostics();
            }

            return options;
        }
    }
}
