namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class GraphContextConfigurationDiagnosticsExtension
    {
        public static TGraphContextConfiguration UseFunctionalGraphContextDiagnostics<TGraphContextConfiguration>(this TGraphContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TGraphContextConfiguration : GraphContextConfiguration
        {
            var extensions = new IGraphContextExtension[]
            {
                new DiagnosticsGraphContextExtension(diagnostics)
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
