// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public static class FunctionalOptionsUseDiagnosticsExtension
    {
        public static FunctionalOptions UseDiagnostics(this FunctionalOptions options)
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

        public static async Task<FunctionalOptions> UseDiagnostics(this Task<FunctionalOptions> optionsTask)
        {
            var options = await optionsTask.ConfigureAwait(false);

            return options.UseDiagnostics();
        }
    }
}
