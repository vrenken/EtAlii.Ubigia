namespace EtAlii.Ubigia.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    internal static class UbigiaDiagnostics
    {
     public static readonly IDiagnosticsConfiguration DefaultConfiguration = new DiagnosticsConfiguration
        {
            EnableProfiling = false,
            EnableLogging = false,
            EnableDebugging = false,
            CreateLogFactory = () => new DisabledLogFactory(),
            CreateLogger = CreateLogger,//factory => factory.Create("EtAlii", "Default"),
            CreateProfilerFactory = () => new DisabledProfilerFactory(),
            CreateProfiler = CreateProfiler,//factory => factory.Create("EtAlii", "Default"),
        };
     
         private static ILogger CreateLogger(ILogFactory factory)
         {
             return factory.Create("EtAlii", "EtAlii.Ubigia");
         }

         private static IProfiler CreateProfiler(IProfilerFactory factory)
         {
             return factory.Create("EtAlii", "EtAlii.Ubigia");
         }
    }
}
