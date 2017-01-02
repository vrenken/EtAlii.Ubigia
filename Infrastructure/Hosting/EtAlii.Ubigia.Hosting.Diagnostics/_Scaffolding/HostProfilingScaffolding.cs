namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class HostProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public HostProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<IProfilerFactory>(() => _diagnostics.CreateProfilerFactory(), Lifestyle.Singleton);
            container.Register<IProfiler>(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()), Lifestyle.Singleton);
            
            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
            }
        }
    }
}
