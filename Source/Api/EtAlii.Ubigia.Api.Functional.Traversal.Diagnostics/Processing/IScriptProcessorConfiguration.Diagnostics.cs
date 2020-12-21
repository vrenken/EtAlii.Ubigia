namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ScriptProcessorConfigurationDiagnosticsExtension
    {
        public static ScriptProcessorConfiguration UseFunctionalDiagnostics(this ScriptProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptProcessorExtension[]
            {
                new DiagnosticsScriptProcessorExtension(diagnostics),
            };

            return configuration.Use(extensions);

        }
    }
}
