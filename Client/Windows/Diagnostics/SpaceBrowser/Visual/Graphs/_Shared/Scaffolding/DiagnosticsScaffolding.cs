namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScaffolding
    {
        public void Register(Container container, IDiagnosticsConfiguration diagnostics, ILogger logger, ILogFactory logFactory)
        {
            container.Register<IDiagnosticsConfiguration>(() => diagnostics);
            container.Register<ILogger>(() => logger);
            container.Register<ILogFactory>(() => logFactory);
        }
    }
}