//namespace EtAlii.Servus.Infrastructure
//{
//    using System;
//    using EtAlii.xTechnology.Logging;

//    public class DiagnosticsFactory
//    {
//        public IDiagnosticsConfiguration Create(
//            bool enableDebugging,
//            bool enableProfiling,
//            bool enableLogging,
//            Func<ILogFactory> createLogFactory,
//            Func<IProfilerFactory> createProfilerFactory,
//            Func<IProfilerFactory, IProfiler> createProfiler,
//            Func<ILogFactory, ILogger> createLogger)
//        {
//            return new DiagnosticsConfiguration
//            {
//                EnableDebugging = enableDebugging,
//                EnableProfiling = enableProfiling,
//                EnableLogging = enableLogging,
//                CreateLogFactory = createLogFactory,
//                CreateProfilerFactory = createProfilerFactory,
//                CreateLogger = createLogger,
//                CreateProfiler = createProfiler,
//            };
//        }

//        public IDiagnosticsConfiguration CreateDisabled(string name, string category)
//        {
//            return Create<DisabledLogFactory, DisabledProfilerFactory>(false, false, false, name, category);
//        }

//        public IDiagnosticsConfiguration Create<TLogFactory, TProfilderFactory>(
//            bool enableDebugging,
//            bool enableProfiling,
//            bool enableLogging,
//            string name, string category)
//            where TLogFactory : ILogFactory, new()
//            where TProfilderFactory : IProfilerFactory, new()
//        {
//            return Create(enableDebugging, enableProfiling, enableLogging,
//                    () => new TLogFactory(),
//                    () => new TProfilderFactory(),
//                    (factory) => factory.Create(name, category),
//                    (factory) => factory.Create(name, category));
//        }
//    }
//}