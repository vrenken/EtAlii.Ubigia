namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.xTechnology.Diagnostics;
    using SimpleInjector;

    public class TestHostProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public TestHostProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
            }
        }
    }
}