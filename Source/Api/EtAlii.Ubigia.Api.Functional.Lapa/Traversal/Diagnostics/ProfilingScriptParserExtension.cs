// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.Ubigia.Diagnostics.Profiling;
using EtAlii.xTechnology.MicroContainer;

public class ProfilingScriptParserExtension : IExtension
{
    private readonly IProfiler _profiler;

    public ProfilingScriptParserExtension(IProfiler profiler)
    {
        _profiler = profiler;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        container.Register(() => _profiler);
        container.RegisterDecorator<IScriptParser, ProfilingScriptParser>();
        container.RegisterDecorator<IPathParser, ProfilingPathParser>();
        container.RegisterDecorator<ISequenceParser, ProfilingSequenceParser>();
        container.RegisterDecorator<INonRootedPathSubjectParser, ProfilingNonRootedPathSubjectParser>();
    }
}
