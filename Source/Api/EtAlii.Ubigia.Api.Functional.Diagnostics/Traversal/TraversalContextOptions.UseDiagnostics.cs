// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical.Diagnostics;

    public static class TraversalContextOptionsDiagnosticsExtension
    {
        public static TTraversalContextOptions UseFunctionalTraversalDiagnostics<TTraversalContextOptions>(this TTraversalContextOptions options, bool alsoUseForDeeperDiagnostics = true)
            where TTraversalContextOptions : FunctionalContextOptions
        {
            var extensions = new ITraversalContextExtension[]
            {
                new LoggingTraversalContextExtension()
            };

            options = options.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                options = options.UseLogicalDiagnostics();
            }

            return options;
        }
    }
}
