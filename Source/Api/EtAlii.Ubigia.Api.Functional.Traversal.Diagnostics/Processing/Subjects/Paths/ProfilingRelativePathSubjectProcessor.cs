namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingRelativePathSubjectProcessor : IRelativePathSubjectProcessor
    {
        private readonly IRelativePathSubjectProcessor _decoree;
        private readonly IProfiler _profiler;
        public ProfilingRelativePathSubjectProcessor(
            IRelativePathSubjectProcessor decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubject);
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("Path subject: " + subject);
            profile.Subject = subject;

            await _decoree.Process(subject, scope, output).ConfigureAwait(false);

            _profiler.End(profile);
        }
    }
}
