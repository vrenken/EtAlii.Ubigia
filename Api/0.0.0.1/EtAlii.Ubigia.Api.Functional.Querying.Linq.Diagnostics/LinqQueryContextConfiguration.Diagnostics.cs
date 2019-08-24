namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.Diagnostics;

    public static class LinqQueryContextConfigurationDiagnosticsExtension 
    {
        public static LinqQueryContextConfiguration UseFunctionalDiagnostics(this LinqQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ILinqQueryContextExtension[]
            {
                new DiagnosticsLinqQueryContextExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}