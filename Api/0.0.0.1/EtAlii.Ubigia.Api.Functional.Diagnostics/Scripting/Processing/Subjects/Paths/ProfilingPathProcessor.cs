namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class ProfilingPathProcessor : IPathProcessor
    {
        private readonly IPathProcessor _decoree;
        private readonly IProfiler _profiler;

        public IProcessingContext Context => _decoree.Context;

        public ProfilingPathProcessor(
            IPathProcessor decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubject);
        }

        public async Task Process(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("Processing path subject: " + pathSubject.ToString());
            profile.PathSubject = pathSubject;

            await _decoree.Process(pathSubject, scope, output);

            _profiler.End(profile);
        }
    }
}