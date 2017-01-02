namespace EtAlii.Servus.Api
{
    using EtAlii.xTechnology.Logging;
    using System;

    public interface IDiagnosticsConfiguration
    {
        bool EnableProfiling { get; set; }
        bool EnableLogging { get; set; }
        bool EnableDebugging { get; set; }
        
        Func<ILogFactory> CreateLogFactory { get; set; }
        Func<IProfilerFactory> CreateProfilerFactory { get; set; }
        Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }
        Func<ILogFactory, ILogger> CreateLogger { get; set; }
    }
}