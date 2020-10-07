namespace EtAlii.Ubigia.Provisioning.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ProvisioningProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ProvisioningProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register(() => _diagnostics.CreateProfilerFactory());
            container.Register(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
                // Invoke all DI container registrations involved in profiling the provisioning subsystem.
            }
        }
    }
}