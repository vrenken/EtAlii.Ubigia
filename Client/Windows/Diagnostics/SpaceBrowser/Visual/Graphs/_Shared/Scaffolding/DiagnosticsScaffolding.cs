namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScaffolding
    {
        public void Register(Container container, IDiagnosticsConfiguration diagnostics, ILogger logger, ILogFactory logFactory)
        {
            container.Register(() => diagnostics);
            container.Register(() => logger);
            container.Register(() => logFactory);
        }
    }
}