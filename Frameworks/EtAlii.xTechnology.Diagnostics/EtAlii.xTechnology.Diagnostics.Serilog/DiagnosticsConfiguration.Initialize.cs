namespace EtAlii.xTechnology.Diagnostics.Serilog
{
    using System;
    using global::Serilog;

    public static class DiagnosticsConfigurationInitializeExtension
    {
        
        public static IDiagnosticsConfiguration UseSerilog(this IDiagnosticsConfiguration configuration)
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
            return configuration;
        }
    }
}
