namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
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
                // Register the interface/class singletons needed for logging.
                // Most of the time this will be decorators in the container.RegisterDecorator pattern.
            }
        }
    }
}