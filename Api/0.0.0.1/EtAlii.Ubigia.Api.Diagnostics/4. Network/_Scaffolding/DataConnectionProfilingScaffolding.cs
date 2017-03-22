namespace EtAlii.Ubigia.Api.Diagnostics
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class DataConnectionProfilingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateProfilerFactory());
            container.Register(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IDataConnection), typeof(ProfilingDataConnection));
            }
        }
    }
}
