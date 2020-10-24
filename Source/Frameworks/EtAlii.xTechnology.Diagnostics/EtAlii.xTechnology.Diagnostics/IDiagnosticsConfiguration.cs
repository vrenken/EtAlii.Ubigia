namespace EtAlii.xTechnology.Diagnostics
{
    using System;

    public interface IDiagnosticsConfiguration
    {
        bool EnableProfiling { get; set; }
        bool EnableLogging { get; set; }
        bool EnableDebugging { get; set; }
        
        Func<IProfilerFactory> CreateProfilerFactory { get; set; }
        Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }
    }
}