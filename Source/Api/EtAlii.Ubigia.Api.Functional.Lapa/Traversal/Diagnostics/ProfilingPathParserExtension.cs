// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingPathParserExtension : IScriptParserExtension
    {
        private readonly IProfiler _profiler;

        public ProfilingPathParserExtension(IProfiler profiler)
        {
            _profiler = profiler;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _profiler);
            container.RegisterDecorator(typeof(IPathParser), typeof(ProfilingPathParser));
            container.RegisterDecorator(typeof(INonRootedPathSubjectParser), typeof(ProfilingNonRootedPathSubjectParser));
        }
    }
}
