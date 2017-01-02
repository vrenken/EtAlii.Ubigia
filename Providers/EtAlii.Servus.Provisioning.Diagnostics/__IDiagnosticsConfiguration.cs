//namespace EtAlii.Servus.Provisioning
//{
//    using System;
//    using EtAlii.xTechnology.Logging;

//    public interface IDiagnosticsConfiguration
//    {
//        bool EnableProfiling { get; set; }
//        bool EnableLogging { get; set; }
//        bool EnableDebugging { get; set; }
        
//        Func<ILogFactory> CreateLogFactory { get; set; }
//        Func<IProfilerFactory> CreateProfilerFactory { get; set; }
//        Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }
//        Func<ILogFactory, ILogger> CreateLogger { get; set; }
//    }
//}