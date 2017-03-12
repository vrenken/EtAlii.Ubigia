namespace EtAlii.Ubigia.Storage
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class ComponentsProfilingScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public ComponentsProfilingScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
                container.RegisterDecorator(typeof(IItemStorage), typeof(ProfilingItemStorage));
                container.RegisterDecorator(typeof(IComponentStorage), typeof(ProfilingComponentStorage));
            }
        }
    }
}
