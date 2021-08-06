// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using System.Reflection;
    using Serilog;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;

    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S4792:Configuring loggers is security-sensitive",
        Justification = "Safe to do so here.")]
    public static class DiagnosticsOptions
    {
        private static readonly LoggerConfiguration _loggerConfiguration = new();

        /// <summary>
        /// Returns a configuration root with additional debug information that can be added on top of existing configuration roots.
        /// </summary>
        public static IConfiguration ConfigurationRoot { get; private set; }

        private static bool _isInitialized;

        public static void Configure(LoggerConfiguration loggerConfiguration, Assembly executingAssembly)
        {
            var executingAssemblyName = executingAssembly.GetName();
            loggerConfiguration.ReadFrom
                .Configuration(ConfigurationRoot)
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
        }

        public static void Initialize(Assembly rootAssembly, IConfiguration diagnosticsConfigurationRoot)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            ConfigurationRoot = diagnosticsConfigurationRoot;

            Configure(_loggerConfiguration, rootAssembly);
            Log.Logger = _loggerConfiguration.CreateLogger();

            // Let's flush the log when the process exits.
            AppDomain.CurrentDomain.ProcessExit += (_, _) => Log.CloseAndFlush();

            // And log all unhandled exceptions.
            AppDomain.CurrentDomain.UnhandledException += (_, e) => Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
        }
    }
}
