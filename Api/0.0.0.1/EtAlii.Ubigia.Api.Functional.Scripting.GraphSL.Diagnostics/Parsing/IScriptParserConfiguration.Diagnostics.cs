namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class ScriptParserConfigurationDiagnosticsExtension 
    {
        public static ScriptParserConfiguration Use(this IScriptParserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptParserExtension[]
            {
                new DiagnosticsScriptParserExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);

        }
    }
}