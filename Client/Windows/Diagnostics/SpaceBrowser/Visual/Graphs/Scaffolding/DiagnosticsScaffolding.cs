namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class DiagnosticsScaffolding
    {
        public void Register(Container container, IDiagnosticsConfiguration diagnostics, ILogger logger, ILogFactory logFactory)
        {
            container.Register<IDiagnosticsConfiguration>(() => diagnostics, Lifestyle.Singleton);
            container.Register<ILogger>(() => logger, Lifestyle.Singleton);
            container.Register<ILogFactory>(() => logFactory, Lifestyle.Singleton);
        }
    }
}