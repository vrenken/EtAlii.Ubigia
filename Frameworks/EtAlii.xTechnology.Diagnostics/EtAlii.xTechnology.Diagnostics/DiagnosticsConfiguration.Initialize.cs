namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using Serilog;

    public partial class DiagnosticsConfiguration
    {
        private static LoggerConfiguration _loggerConfiguration = new LoggerConfiguration();

        public static void Update(Func<LoggerConfiguration, LoggerConfiguration> loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration(_loggerConfiguration);
            Log.Logger = _loggerConfiguration.CreateLogger();
        }

        static DiagnosticsConfiguration()
        {
            Update(loggerConfiguration =>
                loggerConfiguration.MinimumLevel.Verbose()
                    .Enrich.FromLogContext()
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
                    }));

            // Let's flush the log when the process exits.
            AppDomain.CurrentDomain.ProcessExit += (o, e) => Log.CloseAndFlush();

            // And log all unhandled exceptions.
            AppDomain.CurrentDomain.UnhandledException += (o, e) => Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
        }
    }
}
