// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Serilog;

    public class HostLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public HostLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4792:Configuring loggers is security-sensitive",
            Justification = "Safe to do so here.")]
        public void Register(Container container)
        {
            if (_options.InjectLogging) // logging is enabled
            {
                container.RegisterInitializer<IHost>(host =>
                {
                    var configurableHost = (IConfigurableHost)host;
                    configurableHost.ConfigureHost += webHostBuilder => webHostBuilder.UseSerilog((_, loggerConfiguration) => DiagnosticsOptions.ConfigureLoggerConfiguration(loggerConfiguration, System.Reflection.Assembly.GetExecutingAssembly()), true);
                });

                // Register for logging required DI instances.
                container.RegisterDecorator(typeof(IInstanceCreator), typeof(LoggingInstanceCreator));
            }
        }
    }
}
