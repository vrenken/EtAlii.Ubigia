namespace EtAlii.Ubigia.Diagnostics
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using Serilog;

    internal static class UbigiaDiagnostics
    {
        static UbigiaDiagnostics()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessName()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .Enrich.WithMemoryUsage()
                .WriteTo.Async(writeTo =>
                {
                    writeTo.Seq("http://vrenken.duckdns.org:5341");
                    writeTo.Debug();
                })
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
            };
        }
        
        public static readonly IDiagnosticsConfiguration DefaultConfiguration = new DiagnosticsConfiguration
        {
            EnableProfiling = false,
            EnableLogging = true,
            EnableDebugging = true,
            CreateProfilerFactory = () => new DisabledProfilerFactory(),
            CreateProfiler = CreateProfiler,//factory => factory.Create("EtAlii", "Default"),
        };
     
        private static IProfiler CreateProfiler(IProfilerFactory factory)
        {
            return factory.Create("EtAlii", "EtAlii.Ubigia");
        }
    }
}
