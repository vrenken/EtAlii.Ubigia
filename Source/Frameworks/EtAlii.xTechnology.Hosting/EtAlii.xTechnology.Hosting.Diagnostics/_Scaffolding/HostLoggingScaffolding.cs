// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public class HostLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        [SuppressMessage("Sonar Code Smell", "S4792:Configuring loggers is security-sensitive", Justification = "Safe to do so here.")]
        public void Register(Container container)
        {
            if (_diagnostics.EnableLogging) // logging is enabled
            {
                container.RegisterInitializer<IHost>(host =>
                {
                    var configurableHost = (IConfigurableHost)host;
                    configurableHost.ConfigureHost += webHostBuilder => webHostBuilder.UseSerilog((_, loggerConfiguration) => DiagnosticsConfiguration.Configure(loggerConfiguration, System.Reflection.Assembly.GetExecutingAssembly()), true);
                    configurableHost.ConfigureLogging += logging =>
                    {
                        if (!Debugger.IsAttached) return;

                        // SonarQube: Make sure that this logger's configuration is safe.
                        // I think it is as this host is for testing only.
                        //logging.AddDebug[]
                        logging.AddDebug();

                        logging.AddFilter(level => host.ShouldOutputLog && level >= host.LogLevel);
                        logging.AddFilter("Microsoft.AspNetCore.SignalR", level => host.ShouldOutputLog && level >= host.LogLevel);
                        logging.AddFilter("Microsoft.AspNetCore.Http.Connections", level => host.ShouldOutputLog && level >= host.LogLevel);
                        logging.SetMinimumLevel(LogLevel.Trace);

                    };
                });

                // Register for logging required DI instances.
                container.RegisterDecorator(typeof(IInstanceCreator), typeof(LoggingInstanceCreator));
            }
        }
    }
}
