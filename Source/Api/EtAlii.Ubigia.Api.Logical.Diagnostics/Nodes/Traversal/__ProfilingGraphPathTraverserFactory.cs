// // Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.Ubigia.Api.Logical.Diagnostics
// [
//     using EtAlii.Ubigia.Diagnostics.Profiling
//     using EtAlii.xTechnology.MicroContainer
//
//     public sealed class ProfilingGraphPathTraverserFactory : IGraphPathTraverserFactory
//     [
//         private readonly IGraphPathTraverser _decoree
//         private readonly IProfiler _profiler
//
//         public ProfilingGraphPathTraverserFactory(
//             IGraphPathTraverserFactory decoree,
//             IProfiler profiler)
//         [
//             _decoree = decoree
//             _profiler = profiler
//         ]
//
//         public IGraphPathTraverser Create(GraphPathTraverserOptions options)
//         [
//             options.Use(new IGraphPathTraverserExtension[]
//             [
//                 new ProfilingGraphPathTraverserExtension(_profiler),
//             ])
//             return _decoree.Create(options)
//         ]
//     ]
// ]
