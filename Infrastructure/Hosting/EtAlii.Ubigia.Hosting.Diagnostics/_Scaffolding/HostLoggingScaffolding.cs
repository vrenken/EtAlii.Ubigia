namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class HostLoggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostLoggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<ILogFactory>(() => _diagnostics.CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()), Lifestyle.Singleton);
            if (_diagnostics.EnableLogging) // logging is enabled
            {
            }
        }
    }
}