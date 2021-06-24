// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphPathTraverserFactory : IGraphPathTraverserFactory
    {
        private readonly IGraphPathTraverserFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathTraverserFactory(
            IGraphPathTraverserFactory decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IGraphPathTraverser Create(GraphPathTraverserConfiguration configuration)
        {
            configuration.Use(new IGraphPathTraverserExtension[]
            {
                new ProfilingGraphPathTraverserExtension(_profiler), 
            });
            return _decoree.Create(configuration);
        }
    }
}