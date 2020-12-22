namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;

    public static class TraversalScriptContextConfigurationDiagnosticsExtension
    {
        public static TTraversalScriptContextConfiguration UseFunctionalTraversalDiagnostics<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var extensions = new ITraversalScriptContextExtension[]
            {
                new DiagnosticsTraversalScriptContextExtension(diagnostics)
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseLogicalDiagnostics(diagnostics);
            }

            return configuration;
        }
    }
}
