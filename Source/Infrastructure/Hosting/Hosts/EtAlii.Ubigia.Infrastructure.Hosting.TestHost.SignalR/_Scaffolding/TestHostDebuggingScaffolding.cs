namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class TestHostDebuggingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public TestHostDebuggingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableDebugging) // diagnostics is enabled
            {
                // Invoke all DI container registrations involved in debugging the AspNet test host.
            }
        }
    }
}
