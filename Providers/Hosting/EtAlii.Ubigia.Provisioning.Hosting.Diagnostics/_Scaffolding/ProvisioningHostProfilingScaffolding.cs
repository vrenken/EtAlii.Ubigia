namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ProvisioningHostProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningHostProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<IProfilerFactory>(() => _diagnostics.CreateProfilerFactory());
            container.Register<IProfiler>(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
            }
        }
    }
}