namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingScriptProcessor : IProfilingScriptProcessor
    {
        public IProfiler Profiler { get; }

        private readonly IScriptProcessor _decoree;

        public ProfilingScriptProcessor(
            IScriptProcessor decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessor);
        }

        public IObservable<SequenceProcessingResult> Process(Script script)
        {
            return _decoree.Process(script);
        }
    }
}
