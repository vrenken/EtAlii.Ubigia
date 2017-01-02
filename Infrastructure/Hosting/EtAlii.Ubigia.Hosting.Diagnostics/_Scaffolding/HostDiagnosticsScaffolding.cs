namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.xTechnology.Diagnostics;
    using SimpleInjector;

    public class HostDiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostDiagnosticsScaffolding(IDiagnosticsConfiguration diagnostics1)
        {
            _diagnostics = diagnostics1;
        }

        public void Register(Container container)
        {
            container.Register<IDiagnosticsConfiguration>(() => _diagnostics, Lifestyle.Singleton);
        }
    }
}