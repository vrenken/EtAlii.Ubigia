namespace EtAlii.Servus.Provisioning
{
    using EtAlii.Servus.Api;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;
    using SimpleInjector.Extensions;

    internal class ProvisioningProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
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