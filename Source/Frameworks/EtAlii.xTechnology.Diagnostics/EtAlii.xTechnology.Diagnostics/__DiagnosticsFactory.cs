// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// namespace EtAlii.xTechnology.Diagnostics
// [
//     using System
//
//     public class DiagnosticsFactory
//     [
//         public IDiagnosticsConfiguration Create(
//             bool enableDebugging,
//             bool enableProfiling,
//             bool enableLogging,
//             Func<ILogFactory> createLogFactory,
//             Func<IProfilerFactory> createProfilerFactory,
//             Func<IProfilerFactory, IProfiler> createProfiler,
//             Func<ILogFactory, ILogger> createLogger)
//         [
//             return new DiagnosticsConfiguration
//             [
//                 EnableDebugging = enableDebugging,
//                 EnableProfiling = enableProfiling,
//                 EnableLogging = enableLogging,
//                 CreateLogFactory = createLogFactory,
//                 CreateProfilerFactory = createProfilerFactory,
//                 CreateLogger = createLogger,
//                 CreateProfiler = createProfiler,
//             ]
//         ]
//
//         public IDiagnosticsConfiguration CreateDisabled(string name, string category)
//         [
//             return Create<DisabledLogFactory, DisabledProfilerFactory>(false, false, false, name, category)
//         ]
//
//         public IDiagnosticsConfiguration Create<TLogFactory, TProfilerFactory>(
//             bool enableDebugging,
//             bool enableProfiling,
//             bool enableLogging,
//             string name, string category)
//             where TLogFactory : ILogFactory, new()
//             where TProfilerFactory : IProfilerFactory, new()
//         [
//             return Create(enableDebugging, enableProfiling, enableLogging,
//                     () => new TLogFactory(),
//                     () => new TProfilerFactory(),
//                     (factory) => factory.Create(name, category),
//                     (factory) => factory.Create(name, category))
//         ]
//     ]
// ]