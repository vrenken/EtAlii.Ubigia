// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;

    public static class TraversalContextOptionsDiagnosticsExtension
    {
        public static TFunctionalOptions UseFunctionalDiagnostics<TFunctionalOptions>(this TFunctionalOptions options, bool alsoUseForDeeperDiagnostics = true)
            where TFunctionalOptions : FunctionalOptions
        {
            var extensions = new IFunctionalExtension[]
            {

                new DiagnosticsScriptParserExtension(options.ConfigurationRoot),
                new DiagnosticsScriptProcessorExtension(options.ConfigurationRoot),

                new LoggingTraversalContextExtension(options.ConfigurationRoot),
                new LoggingSchemaProcessorExtension(options.ConfigurationRoot),
                new LoggingGraphContextExtension(options.ConfigurationRoot),

                new ProfilingSchemaProcessorExtension(options.ConfigurationRoot),
                new ProfilingGraphContextExtension(options.ConfigurationRoot),
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
