// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public static class TraversalContextOptionsDiagnosticsExtension
    {
        public static TTraversalContextOptions UseFunctionalTraversalDiagnostics<TTraversalContextOptions>(this TTraversalContextOptions options, IConfiguration configurationRoot, bool alsoUseForDeeperDiagnostics = true)
            where TTraversalContextOptions : FunctionalContextOptions
        {
            var extensions = new ITraversalContextExtension[]
            {
                new LoggingTraversalContextExtension(configurationRoot)
            };

            options = options.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                options = options.UseLogicalDiagnostics(configurationRoot);
            }

            return options;
        }
    }
}
