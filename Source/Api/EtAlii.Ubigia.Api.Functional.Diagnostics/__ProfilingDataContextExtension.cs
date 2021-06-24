// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Api.Functional.Diagnostics
//[
//    using EtAlii.Ubigia.Diagnostics.Profiling
//    using EtAlii.Ubigia.Api.Functional
//    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
//    using EtAlii.xTechnology.MicroContainer
//
//    public class ProfilingDataContextExtension : IDataContextExtension
//    [
//        public void Initialize(Container container)
//        [
//            container.RegisterDecorator(typeof(IDataContext), typeof(ProfilingDataContext))
////            container.RegisterDecorator(typeof(ITraversalContext), typeof(ProfilingTraversalContext))
////
////            container.RegisterDecorator(typeof(IScriptProcessorFactory), typeof(ProfilingScriptProcessorFactory))
////            container.RegisterDecorator(typeof(IScriptParserFactory), typeof(ProfilingScriptParserFactory))
//
//            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.Context))
//        ]
//    ]
//]
