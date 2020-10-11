namespace EtAlii.Ubigia.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Diagnostics.Serilog;

    internal static class UbigiaDiagnostics
    {
     public static readonly IDiagnosticsConfiguration DefaultConfiguration = new DiagnosticsConfiguration
        {
            EnableProfiling = false,
            EnableLogging = true,
            EnableDebugging = true,
            CreateLogFactory = () => new SerilogLogFactory(),
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
