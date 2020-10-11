namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class HostLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableLogging) // logging is enabled
            {
                // Register for logging required DI instances.
                container.Register<IInstanceCreator, InstanceCreator>();
            }
        }
    }
}