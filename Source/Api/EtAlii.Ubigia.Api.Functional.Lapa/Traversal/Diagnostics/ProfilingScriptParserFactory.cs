// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingScriptParserFactory : IScriptParserFactory
    {
        private readonly IScriptParserFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingScriptParserFactory(
            IScriptParserFactory decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IScriptParser Create(TraversalParserConfiguration configuration)
        {
            configuration.Use(new IScriptParserExtension[]
            {
                new ProfilingScriptParserExtension(_profiler),
            });

            return _decoree.Create(configuration);
        }
    }
}
