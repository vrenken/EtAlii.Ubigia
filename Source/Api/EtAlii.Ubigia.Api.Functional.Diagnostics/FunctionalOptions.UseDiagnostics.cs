// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public static class FunctionalOptionsUseDiagnosticsExtension
    {
        public static TFunctionalOptions UseFunctionalDiagnostics<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            var extensions = new IExtension[]
            {

                new DiagnosticsScriptParserExtension(options.ConfigurationRoot),
                new DiagnosticsScriptProcessorExtension(options.ConfigurationRoot),

                new LoggingTraversalContextExtension(options.ConfigurationRoot),
                new LoggingSchemaProcessorExtension(options.ConfigurationRoot),
                new LoggingGraphContextExtension(options.ConfigurationRoot),

                new ProfilingSchemaProcessorExtension(options.ConfigurationRoot),
                new ProfilingGraphContextExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
