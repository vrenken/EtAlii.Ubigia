// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using System.Reflection;
    using Serilog;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using Microsoft.Extensions.Configuration;

    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S4792:Configuring loggers is security-sensitive",
        Justification = "Safe to do so here.")]
    public partial class DiagnosticsOptions
    {
        private static readonly LoggerConfiguration _loggerConfiguration = new();

        /// <summary>
        /// Returns a configuration root with additional debug information that can be added on top of existing configuration roots.
        /// </summary>
        public static IConfigurationRoot ConfigurationRoot { get; private set; }

        private static bool _isInitialized;
        private static Assembly _entryAssembly;

        public static void ConfigureLoggerConfiguration(LoggerConfiguration loggerConfiguration, IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            var hostName = Dns.GetHostName();
            var entryAssemblyName = _entryAssembly.GetName();
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
                .Enrich.WithProperty("HostName", hostName) // We want to be able to filter the Seq logs depending on the (docker) system they originate from.
                .Enrich.WithProperty("RootAssemblyName", entryAssemblyName.Name)
                .Enrich.WithProperty("RootAssemblyVersion", entryAssemblyName.Version)
                .Enrich.WithMemoryUsage()
                .Enrich.WithProperty("UniqueProcessId", Guid.NewGuid()); // An int process ID is not enough
        }

        public static void Initialize(Assembly entryAssembly, IConfigurationRoot diagnosticsConfigurationRoot)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            _entryAssembly = entryAssembly;

            ConfigureLoggerConfiguration(_loggerConfiguration, diagnosticsConfigurationRoot);
            Log.Logger = _loggerConfiguration.CreateLogger();

            // Let's flush the log when the process exits.
            AppDomain.CurrentDomain.ProcessExit += (_, _) => Log.CloseAndFlush();

            // And log all unhandled exceptions.
            AppDomain.CurrentDomain.UnhandledException += (_, e) => Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
        }
    }
}
