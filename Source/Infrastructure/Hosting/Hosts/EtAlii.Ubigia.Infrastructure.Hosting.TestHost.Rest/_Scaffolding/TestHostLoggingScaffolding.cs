// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public TestHostLoggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectLogging) // logging is enabled.
            {
                //container.RegisterDecorator(typeof(IInfrastructureClient), typeof(LoggingInfrastructureClient), Lifestyle.Singleton)
            }
        }
    }
}
