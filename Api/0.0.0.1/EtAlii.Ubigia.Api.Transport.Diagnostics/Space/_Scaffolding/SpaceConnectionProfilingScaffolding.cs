namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    internal class SpaceConnectionProfilingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            var diagnostics = container.GetInstance<IDiagnosticsConfiguration>();

            container.Register(() => diagnostics.CreateProfilerFactory());
            container.Register(() => diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));

            if (diagnostics.EnableProfiling) // profiling is enabled
            {
                // Register the interface/class singletons needed for profiling.
                // Most of the time this will be decorators in the container.RegisterDecorator pattern.
            }
        }
    }
}
