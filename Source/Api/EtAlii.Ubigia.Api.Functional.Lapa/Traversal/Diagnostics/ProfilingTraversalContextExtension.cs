// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.Ubigia.Diagnostics.Profiling;
using EtAlii.xTechnology.MicroContainer;

public class ProfilingTraversalContextExtension : IExtension
{
    public void Initialize(IRegisterOnlyContainer container)
    {
        container.RegisterDecorator<ITraversalContext, ProfilingTraversalContext>();

        //container.RegisterDecorator<IScriptProcessorFactory, ProfilingScriptProcessorFactory>()
        //container.RegisterDecorator<IScriptParserFactory, ProfilingScriptParserFactory>()

        container.Register<IProfiler>(() => new Profiler(ProfilingAspects.Functional.TraversalContext));
    }
}
