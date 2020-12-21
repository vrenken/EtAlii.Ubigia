namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingPathProcessor : IPathProcessor
    {
        private readonly IPathProcessor _decoree;
        private readonly IProfiler _profiler;

        public IScriptProcessingContext Context => _decoree.Context;

        public ProfilingPathProcessor(
            IPathProcessor decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubject);
        }

        public async Task Process(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("Processing path subject: " + pathSubject);
            profile.PathSubject = pathSubject;

            await _decoree.Process(pathSubject, scope, output).ConfigureAwait(false);

            _profiler.End(profile);
        }
    }
}
