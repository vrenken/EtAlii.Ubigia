// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class SchemaProcessorOptionsUseFunctionalDiagnosticsExtension
    {
        public static SchemaProcessorOptions UseFunctionalDiagnostics(this SchemaProcessorOptions options)
        {
            var extensions = new ISchemaProcessorExtension[]
            {
                new LoggingSchemaProcessorExtension(),
                new ProfilingSchemaProcessorExtension(),
            };

            return options.Use(extensions);

        }
    }
}
