namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public static class GraphTLQueryContextConfigurationDiagnosticsExtension
    {
        public static TGraphTLQueryContextConfiguration UseFunctionalGraphTLDiagnostics<TGraphTLQueryContextConfiguration>(this TGraphTLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TGraphTLQueryContextConfiguration : GraphTLQueryContextConfiguration
        {
            var extensions = new IGraphTLQueryContextExtension[]
            {
                new DiagnosticsGraphTLQueryContextExtension(diagnostics)
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFunctionalGraphSLDiagnostics(diagnostics);
            }

            return configuration;
        }
    }
}
