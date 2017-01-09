namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using EtAlii.Ubigia.Api;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;
    using SimpleInjector;

    public class DiagnosticsScaffolding : IScaffolding
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        public DiagnosticsScaffolding(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Register(Container container)
        {
            container.Register<IDiagnosticsConfiguration>(() => _diagnostics, Lifestyle.Singleton);

            container.Register<IProfilerFactory>(() => _diagnostics.CreateProfilerFactory(), Lifestyle.Singleton);
            container.Register<IProfiler>(() => _diagnostics.CreateProfiler(container.GetInstance<IProfilerFactory>()), Lifestyle.Singleton);
            if (_diagnostics.EnableProfiling) // profiling is enabled
            {
            }

            container.Register<ILogFactory>(() => _diagnostics.CreateLogFactory(), Lifestyle.Singleton);
            container.Register<ILogger>(() => _diagnostics.CreateLogger(container.GetInstance<ILogFactory>()), Lifestyle.Singleton);
            if (_diagnostics.EnableLogging) // logging is enabled
            {
            }

            if (_diagnostics.EnableDebugging) // debugging is enabled
            {
            }
        }
    }
}
