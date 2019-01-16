namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphSLScriptContextConfigurationDiagnosticsExtension 
    {
        public static IGraphSLScriptContextConfiguration Use(this IGraphSLScriptContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IGraphSLScriptContextExtension[]
            {
                new DiagnosticsGraphSLScriptContextExtension(diagnostics)
            };
            return configuration.Use(extensions);
        }
    }
}