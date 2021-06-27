// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingTraversalContextExtension : ITraversalContextExtension
    {
        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(ITraversalContext), typeof(ProfilingTraversalContext));

            container.RegisterDecorator(typeof(IScriptProcessorFactory), typeof(ProfilingScriptProcessorFactory));
            container.RegisterDecorator(typeof(IScriptParserFactory), typeof(ProfilingScriptParserFactory));
            container.RegisterDecorator(typeof(IPathParserFactory), typeof(ProfilingPathParserFactory));

            container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.TraversalContext));
        }
    }
}
