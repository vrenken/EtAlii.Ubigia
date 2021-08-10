// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsOptions _options;

        public TestHostLoggingScaffolding(DiagnosticsOptions options)
        {
            _options = options;
        }

        public void Register(IRegisterOnlyContainer container)
        {
            if (_options.InjectLogging) // logging is enabled.
            {
                //container.RegisterDecorator(typeof(IInfrastructureClient), typeof(LoggingInfrastructureClient), Lifestyle.Singleton)
            }
        }
    }
}
