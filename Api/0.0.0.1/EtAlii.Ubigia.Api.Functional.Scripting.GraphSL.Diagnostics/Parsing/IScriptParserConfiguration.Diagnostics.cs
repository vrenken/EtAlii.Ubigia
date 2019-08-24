namespace EtAlii.Ubigia.Api.Functional.Scripting
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