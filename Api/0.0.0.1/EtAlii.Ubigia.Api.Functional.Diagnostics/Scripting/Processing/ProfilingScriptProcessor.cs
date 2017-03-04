namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using EtAlii.Ubigia.Api.Functional;

    internal class ProfilingScriptProcessor : IProfilingScriptProcessor
    {
        public IProfiler Profiler => _profiler;
        private readonly IProfiler _profiler;

        private readonly IScriptProcessor _decoree;

        public ProfilingScriptProcessor(
            IScriptProcessor decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = _profiler.Create(ProfilingAspects.Functional.ScriptProcessor);
        }

        public IObservable<SequenceProcessingResult> Process(Script script)
        {
            return _decoree.Process(script);
        }
    }
}