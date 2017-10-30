namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

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