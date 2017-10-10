namespace EtAlii.Servus.Hosting
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Infrastructure;
    using SimpleInjector;

    public class HostDebuggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostDebuggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableDebugging) // debugging is enabled
            {
            }
        }
    }
}