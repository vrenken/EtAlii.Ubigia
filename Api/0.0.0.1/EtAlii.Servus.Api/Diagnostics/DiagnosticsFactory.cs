namespace EtAlii.Servus.Api
{
    using EtAlii.xTechnology.Logging;
    using System;

    public class DiagnosticsFactory
    {
        public IDiagnosticsConfiguration Create(
            bool enableDebugging,
            bool enableProfiling,
            bool enableLogging,
            Func<ILogFactory> createLogFactory,
            Func<IProfilerFactory> createProfilerFactory,
            Func<IProfilerFactory, IProfiler> createProfiler,
            Func<ILogFactory, ILogger> createLogger)
        {
            return new DiagnosticsConfiguration
            {
                EnableDebugging = enableDebugging,
                EnableProfiling = enableProfiling,
                EnableLogging = enableLogging,
                CreateLogFactory = createLogFactory,
                CreateProfilerFactory = createProfilerFactory,
                CreateLogger = createLogger,
                CreateProfiler = createProfiler,
            };
        }
    }
}