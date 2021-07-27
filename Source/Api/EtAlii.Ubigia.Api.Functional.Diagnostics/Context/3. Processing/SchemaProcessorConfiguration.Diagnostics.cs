// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Microsoft.Extensions.Configuration;

    public static class SchemaProcessorConfigurationDiagnosticsExtension
    {
        public static SchemaProcessorConfiguration UseFunctionalDiagnostics(this SchemaProcessorConfiguration configuration, IConfiguration configurationRoot)
        {
            var extensions = new ISchemaProcessorExtension[]
            {
                new LoggingSchemaProcessorExtension(configurationRoot),
                new ProfilingSchemaProcessorExtension(configurationRoot),
            };

            return configuration.Use(extensions);

        }
    }
}
