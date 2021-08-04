// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Microsoft.Extensions.Configuration;

    public static class SchemaProcessorOptionsDiagnosticsExtension
    {
        public static SchemaProcessorOptions UseFunctionalDiagnostics(this SchemaProcessorOptions options, IConfiguration configurationRoot)
        {
            var extensions = new ISchemaProcessorExtension[]
            {
                new LoggingSchemaProcessorExtension(configurationRoot),
                new ProfilingSchemaProcessorExtension(configurationRoot),
            };

            return options.Use(extensions);

        }
    }
}
