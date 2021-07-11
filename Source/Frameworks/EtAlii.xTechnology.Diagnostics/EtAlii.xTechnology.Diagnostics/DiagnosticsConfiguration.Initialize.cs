// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using System.Reflection;
    using Serilog;
    using Serilog.Events;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;

    [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
    public partial class DiagnosticsConfiguration
    {
        private static readonly LoggerConfiguration _loggerConfiguration = new();

        private static bool _isInitialized;
        public static void Configure(LoggerConfiguration loggerConfiguration, Assembly executingAssembly, IConfigurationRoot configurationRoot)
        {
            var executingAssemblyName = executingAssembly.GetName();
            loggerConfiguration.ReadFrom
                .Configuration(configurationRoot)
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
                .Enrich.WithProperty("UniqueProcessId", Guid.NewGuid()); // An int process ID is not enough
            //
            // loggerConfiguration
            //     .WriteTo.Async(writeTo =>
            //     {
            //         var seqConfiguration = configurationRoot.GetSection("Logging:Output:Seq");
            //         if (seqConfiguration.GetValue<bool>("Enabled"))
            //         {
            //             var logLevel = GetLogLevel(seqConfiguration);
            //             var url = seqConfiguration.GetValue<string>("Address");
            //             writeTo.Seq(url, logLevel);
            //         }
            //
            //         var consoleConfiguration = configurationRoot.GetSection("Logging:Output:Console");
            //         if (consoleConfiguration.GetValue<bool>("Enabled"))
            //         {
            //             var logLevel = GetLogLevel(consoleConfiguration);
            //             writeTo.Debug(logLevel);
            //         }
            //     });
        }

        private static LogEventLevel GetLogLevel(IConfigurationSection configurationSection)
        {
            return configurationSection.GetValue<string>("LogLevel") switch
            {
                "Fatal" => LogEventLevel.Fatal,
                "Error" => LogEventLevel.Error,
                "Warning" => LogEventLevel.Warning,
                "Information" => LogEventLevel.Information,
                "Info" => LogEventLevel.Information,
                "Debug" => LogEventLevel.Debug,
                "Verbose" => LogEventLevel.Verbose,
                _ => throw new NotSupportedException("Unable to determine LogLevel from configuration section")
            };
        }

        public static void Initialize(Assembly rootAssembly, IConfigurationRoot configurationRoot)
        {
            if (_isInitialized) return;

            _isInitialized = true;

            Configure(_loggerConfiguration, rootAssembly, configurationRoot);
            //_loggerConfiguration = loggerConfiguration[_loggerConfiguration]
            Log.Logger = _loggerConfiguration.CreateLogger();

            // Let's flush the log when the process exits.
            AppDomain.CurrentDomain.ProcessExit += (_, _) => Log.CloseAndFlush();

            // And log all unhandled exceptions.
            AppDomain.CurrentDomain.UnhandledException += (_, e) => Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
        }
    }
}
