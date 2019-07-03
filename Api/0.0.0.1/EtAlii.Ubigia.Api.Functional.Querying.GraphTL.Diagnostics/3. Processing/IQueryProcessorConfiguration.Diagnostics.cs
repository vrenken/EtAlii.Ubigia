namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Querying 
{
    using EtAlii.xTechnology.Diagnostics;

    public static class QueryProcessorConfigurationDiagnosticsExtension
    {
        public static QueryProcessorConfiguration UseFunctionalDiagnostics(this QueryProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IQueryProcessorExtension[]
            {
                //new DiagnosticsQueryProcessorExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);

        }
    }
}