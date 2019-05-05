namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class ScriptProcessorConfigurationDiagnosticsExtension
    {
        public static ScriptProcessorConfiguration Use(this IScriptProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IScriptProcessorExtension[]
            {
                new DiagnosticsScriptProcessorExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);

        }
    }
}