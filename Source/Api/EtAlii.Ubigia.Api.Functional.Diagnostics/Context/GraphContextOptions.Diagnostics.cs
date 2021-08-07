// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class GraphContextOptionsDiagnosticsExtension
    {
        public static TFunctionalOptions UseFunctionalGraphContextDiagnostics<TFunctionalOptions>(this TFunctionalOptions options, bool alsoUseForDeeperDiagnostics = true)
            where TFunctionalOptions : FunctionalOptions
        {
            var extensions = new IGraphContextExtension[]
            {
                new LoggingGraphContextExtension(),
                new ProfilingGraphContextExtension(),
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
