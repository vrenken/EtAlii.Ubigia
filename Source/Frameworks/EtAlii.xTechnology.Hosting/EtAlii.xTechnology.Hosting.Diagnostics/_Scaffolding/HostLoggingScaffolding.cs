// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public class HostLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;
        private readonly IConfigurationRoot _configurationRoot;

        public HostLoggingScaffolding(DiagnosticsOptions options, IConfigurationRoot configurationRoot)
        {
            _options = options;
            _configurationRoot = configurationRoot;
        }

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S4792:Configuring loggers is security-sensitive",
            Justification = "Safe to do so here.")]
        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectLogging) // logging is enabled
            {
                container.RegisterInitializer<IHost>(host =>
                {
                    host.ConfigureHost += webHostBuilder => webHostBuilder.UseSerilog((_, loggerConfiguration) =>
                    {
                        DiagnosticsOptions.ConfigureLoggerConfiguration(loggerConfiguration, System.Reflection.Assembly.GetExecutingAssembly(), _configurationRoot);
                    }, true);
                });
            }
        }
    }
}
