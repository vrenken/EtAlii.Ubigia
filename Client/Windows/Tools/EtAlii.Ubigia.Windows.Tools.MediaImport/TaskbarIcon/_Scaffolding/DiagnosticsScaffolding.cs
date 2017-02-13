namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public DiagnosticsScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<IDiagnosticsConfiguration>(() => _diagnostics);

            container.Register<IProfilerFactory>(() => _diagnostics.CreateProfilerFactory());
            container.Register<IProfiler>(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));
            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
            }

            container.Register<ILogFactory>(() => _diagnostics.CreateLogFactory());
            container.Register<ILogger>(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));
            if (_diagnostics.EnableLogging) // logging is enabled
            {
            }

            if (_diagnostics.EnableDebugging) // debugging is enabled
            {
            }
        }
    }
}
