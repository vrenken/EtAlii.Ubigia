namespace EtAlii.Ubigia.Api.Management
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class StorageConnectionProfilingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register<IProfilerFactory>(() => diagnostics.CreateProfilerFactory());
            container.Register<IProfiler>(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                //container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection));
            }
        }
    }
}
