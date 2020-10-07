namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    internal class ManagementConnectionProfilingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateProfilerFactory());
            container.Register(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));
            
            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IManagementConnection), typeof(ProfilingManagementConnection));
            }

        }
    }
}
