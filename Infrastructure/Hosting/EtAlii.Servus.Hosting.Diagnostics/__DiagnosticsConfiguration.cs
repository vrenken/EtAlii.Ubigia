//namespace EtAlii.Servus.Hosting
//{
//    using System;
//    using EtAlii.xTechnology.Logging;

//    public class DiagnosticsConfiguration : IDiagnosticsConfiguration
//    {
//        public bool EnableProfiling { get; set; }
//        public bool EnableLogging { get; set; }
//        public bool EnableDebugging { get; set; }
        
//        public Func<ILogFactory> CreateLogFactory { get; set; }
//        public Func<IProfilerFactory> CreateProfilerFactory { get; set; }
//        public Func<IProfilerFactory, IProfiler> CreateProfiler { get; set; }
//        public Func<ILogFactory, ILogger> CreateLogger { get; set; }

//        public static readonly DiagnosticsConfiguration Default = new DiagnosticsConfiguration
//        {
//            EnableProfiling = false,
//            EnableLogging = false,
//            EnableDebugging = false,
//            CreateLogFactory = () => new DisabledLogFactory(),
//            CreateLogger = factory => factory.Create("EtAlii", "Default"),
//            CreateProfilerFactory = () => new DisabledProfilerFactory(),
//            CreateProfiler = factory => factory.Create("EtAlii", "Default"),
//        };
//    }
//}