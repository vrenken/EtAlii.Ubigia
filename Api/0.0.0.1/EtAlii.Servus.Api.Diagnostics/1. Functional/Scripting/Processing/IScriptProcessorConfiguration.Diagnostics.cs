namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IScriptProcessorConfigurationDiagnosticsExtension
    {
        public static IScriptProcessorConfiguration Use(this IScriptProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptProcessorExtension[]
            {
                new DiagnosticsScriptProcessorExtension(diagnostics), 
            };
            return configuration.Use(extensions);

        }
    }
}