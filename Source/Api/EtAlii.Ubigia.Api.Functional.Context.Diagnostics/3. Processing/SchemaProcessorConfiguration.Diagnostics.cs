namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using System;
    using EtAlii.xTechnology.Diagnostics;

    public static class SchemaProcessorConfigurationDiagnosticsExtension
    {
        public static SchemaProcessorConfiguration UseFunctionalDiagnostics(this SchemaProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = Array.Empty<ISchemaProcessorExtension>();
            // var extensions = new ISchemaProcessorExtension[]
            // [
            //     new DiagnosticsQueryProcessorExtension(diagnostics),
            // ]

            return configuration.Use(extensions);

        }
    }
}
