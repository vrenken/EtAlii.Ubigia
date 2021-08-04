// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingPathParserFactory : IPathParserFactory
    {
        private readonly IPathParserFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingPathParserFactory(
            IPathParserFactory decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IPathParser Create(TraversalParserOptions options)
        {
            options.Use(new IScriptParserExtension[]
            {
                new ProfilingPathParserExtension(_profiler),
            });

            return _decoree.Create(options);
        }
    }
}
