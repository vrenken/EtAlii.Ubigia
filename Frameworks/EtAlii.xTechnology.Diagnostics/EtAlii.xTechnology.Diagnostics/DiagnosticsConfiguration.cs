namespace EtAlii.xTechnology.Diagnostics
{
    using System;

    public class DiagnosticsConfiguration : IDiagnosticsConfiguration
    {
        public bool EnableProfiling { get; set; }
        public bool EnableLogging { get; set; }
        public bool EnableDebugging { get; set; }
        
        public Func<IProfilerFactory> CreateProfilerFactory { get; set; }
        public Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }

        public static readonly DiagnosticsConfiguration Default = new DiagnosticsConfiguration
        {
            EnableProfiling = false,
            EnableLogging = false,
            EnableDebugging = false,

            CreateProfilerFactory = () => new DisabledProfilerFactory(),
            CreateProfiler = factory => factory.Create("EtAlii", "Default"),
        };
    }
}