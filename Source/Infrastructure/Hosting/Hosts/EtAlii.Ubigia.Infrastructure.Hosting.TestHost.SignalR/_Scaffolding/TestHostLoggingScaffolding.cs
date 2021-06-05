namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public TestHostLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableLogging) // logging is enabled.
            {
                //container.RegisterDecorator(typeof(IInfrastructureClient), typeof(LoggingInfrastructureClient), Lifestyle.Singleton)
            }
        }
    }
}
