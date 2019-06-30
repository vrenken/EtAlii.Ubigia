namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphTLQueryContextConfigurationDiagnosticsExtension 
    {
        public static TGraphTLQueryContextConfiguration UseFunctionalGraphTLDiagnostics<TGraphTLQueryContextConfiguration>(this TGraphTLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TGraphTLQueryContextConfiguration : GraphTLQueryContextConfiguration
        {
            var extensions = new IGraphTLQueryContextExtension[]
            {
                new DiagnosticsGraphTLQueryContextExtension(diagnostics)
            };
            return configuration.Use(extensions);
        }
    }
}