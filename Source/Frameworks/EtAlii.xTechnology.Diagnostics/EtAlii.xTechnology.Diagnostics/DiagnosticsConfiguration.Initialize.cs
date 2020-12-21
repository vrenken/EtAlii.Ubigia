namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using System.Reflection;
    using Serilog;
    using Serilog.Events;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
    public partial class DiagnosticsConfiguration
    {
        private static readonly LoggerConfiguration _loggerConfiguration = new();

        public static void Configure(LoggerConfiguration loggerConfiguration)
        {
            var executingAssemblyName = Assembly.GetCallingAssembly().GetName();

            loggerConfiguration.MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessName()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                // These ones do not give elegant results during unit tests.
                // .Enrich.WithAssemblyName()
                // .Enrich.WithAssemblyVersion()
                // Let's do it ourselves.
                .Enrich.WithProperty("RootAssemblyName", executingAssemblyName.Name)
                .Enrich.WithProperty("RootAssemblyVersion", executingAssemblyName.Version)
                .Enrich.WithMemoryUsage()
                .Enrich.WithProperty("UniqueProcessId", Guid.NewGuid()) // An int process ID is not enough
                .WriteTo.Async(writeTo =>
                {
                    writeTo.Seq("http://vrenken.duckdns.org:5341");
                    writeTo.Debug(LogEventLevel.Error);
                });
        }
        static DiagnosticsConfiguration()
        {
            Configure(_loggerConfiguration);
            //_loggerConfiguration = loggerConfiguration(_loggerConfiguration);
            Log.Logger = _loggerConfiguration.CreateLogger();


            // Let's flush the log when the process exits.
            AppDomain.CurrentDomain.ProcessExit += (_, _) => Log.CloseAndFlush();

            // And log all unhandled exceptions.
            AppDomain.CurrentDomain.UnhandledException += (_, e) => Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
        }
    }
}
