namespace EtAlii.Servus.Storage
{
    using EtAlii.xTechnology.Logging;
    using System;

    public class DiagnosticsConfiguration : IDiagnosticsConfiguration
    {
        public bool EnableProfiling { get; set; }
        public bool EnableLogging { get; set; }
        public bool EnableDebugging { get; set; }
        
        public Func<ILogFactory> CreateLogFactory { get; set; }
        public Func<IProfilerFactory> CreateProfilerFactory { get; set; }
        public Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }
        public Func<ILogFactory, ILogger> CreateLogger { get; set; }
    }
}