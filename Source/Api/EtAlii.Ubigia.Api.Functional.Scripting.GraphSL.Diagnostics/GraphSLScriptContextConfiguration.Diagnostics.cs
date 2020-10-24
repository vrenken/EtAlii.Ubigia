namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphSLScriptContextConfigurationDiagnosticsExtension 
    {
        public static TGraphSLScriptContextConfiguration UseFunctionalGraphSLDiagnostics<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
        {
            var extensions = new IGraphSLScriptContextExtension[]
            {
                new DiagnosticsGraphSLScriptContextExtension(diagnostics)
            };
            return configuration.Use(extensions);
        }
    }
}