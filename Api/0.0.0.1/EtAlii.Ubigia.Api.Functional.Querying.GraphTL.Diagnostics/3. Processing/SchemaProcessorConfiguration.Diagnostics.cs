namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Querying 
{
    using EtAlii.xTechnology.Diagnostics;

    public static class SchemaProcessorConfigurationDiagnosticsExtension
    {
        public static SchemaProcessorConfiguration UseFunctionalDiagnostics(this SchemaProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ISchemaProcessorExtension[]
            {
                //new DiagnosticsQueryProcessorExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);

        }
    }
}