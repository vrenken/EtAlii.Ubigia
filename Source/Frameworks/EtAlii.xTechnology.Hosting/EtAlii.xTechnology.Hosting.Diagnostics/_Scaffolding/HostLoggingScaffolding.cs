// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
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
                    var configuration = container.GetInstance<IHostConfiguration>();
                    var configurableHost = (IConfigurableHost)host;
                    configurableHost.ConfigureHost += webHostBuilder => webHostBuilder.UseSerilog((_, loggerConfiguration) => DiagnosticsConfiguration.Configure(loggerConfiguration, System.Reflection.Assembly.GetExecutingAssembly(), configuration.Root), true);
                });

                // Register for logging required DI instances.
                container.RegisterDecorator(typeof(IInstanceCreator), typeof(LoggingInstanceCreator));
            }
        }
    }
}
