namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class LinqQueryContextConfigurationDiagnosticsExtension
    {
        public static LinqQueryContextConfiguration UseFunctionalDiagnostics(this LinqQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
        {
            var extensions = new ILinqQueryContextExtension[]
            {
                new DiagnosticsLinqQueryContextExtension(diagnostics),
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
