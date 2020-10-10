namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class HostProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register(() => _diagnostics.CreateProfilerFactory());
            container.Register(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));
            
            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
                // Register for profiling required DI instances.
            }
        }
    }
}
