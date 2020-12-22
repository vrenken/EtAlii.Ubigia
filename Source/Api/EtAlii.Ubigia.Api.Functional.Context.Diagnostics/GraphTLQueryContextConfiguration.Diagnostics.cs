namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class GraphXLQueryContextConfigurationDiagnosticsExtension
    {
        public static TGraphXLQueryContextConfiguration UseFunctionalGraphXLDiagnostics<TGraphXLQueryContextConfiguration>(this TGraphXLQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TGraphXLQueryContextConfiguration : GraphXLQueryContextConfiguration
        {
            var extensions = new IGraphXLQueryContextExtension[]
            {
                new DiagnosticsGraphXLQueryContextExtension(diagnostics)
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFunctionalTraversalDiagnostics(diagnostics);
            }

            return configuration;
        }
    }
}
