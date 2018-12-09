namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ScriptProcessorConfigurationDiagnosticsExtension
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