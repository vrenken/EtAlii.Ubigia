namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;

    public static class GraphSLScriptContextConfigurationDiagnosticsExtension
    {
        public static TGraphSLScriptContextConfiguration UseFunctionalGraphSLDiagnostics<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
        {
            var extensions = new IGraphSLScriptContextExtension[]
            {
                new DiagnosticsGraphSLScriptContextExtension(diagnostics)
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
