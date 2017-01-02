namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class ManagementConnectionProfilingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register<IProfilerFactory>(() => diagnostics.CreateProfilerFactory());
            container.Register<IProfiler>(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));
            
            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IManagementConnection), typeof(ProfilingManagementConnection));
            }

        }
    }
}
