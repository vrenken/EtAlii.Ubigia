namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
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
            container.Register(() => _diagnostics);

            container.Register(() => _diagnostics.CreateProfilerFactory());
            container.Register(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()));
            if (_diagnostics.EnableProfiling) 
            {
                // profiling is enabled
            }

            container.Register(() => _diagnostics.CreateLogFactory());
            container.Register(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()));
            if (_diagnostics.EnableLogging) 
            {
                // logging is enabled
            }

            if (_diagnostics.EnableDebugging) 
            {
                // debugging is enabled
            }
        }
    }
}
