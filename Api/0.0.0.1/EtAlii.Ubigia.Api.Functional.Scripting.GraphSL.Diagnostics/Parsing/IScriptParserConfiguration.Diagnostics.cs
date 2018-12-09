namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ScriptParserConfigurationDiagnosticsExtension 
    {
        public static IScriptParserConfiguration Use(this IScriptParserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(diagnostics), 
            };
            return configuration.Use(extensions);

        }
    }
}