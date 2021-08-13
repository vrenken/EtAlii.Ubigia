// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingScriptProcessorFactory : IScriptProcessorFactory
    {
        private readonly IScriptProcessorFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingScriptProcessorFactory(
            IScriptProcessorFactory decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IScriptProcessor Create(FunctionalOptions options)
        {
            options.Use(new IFunctionalExtension[]
            {
                new ProfilingFunctionalExtension2(_profiler),
            });

            return _decoree.Create(options);
        }
    }
}
