namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ScriptParserConfigurationDiagnosticsExtension
    {
        public static ScriptParserConfiguration UseFunctionalDiagnostics(this ScriptParserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(diagnostics),
            };

            return configuration.Use(extensions);

        }
    }
}
