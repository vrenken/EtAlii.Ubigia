namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using EtAlii.Ubigia.Api.Functional;

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

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("Path subject: " + subject);
            profile.Subject = subject;

            _decoree.Process(subject, scope, output);

            _profiler.End(profile);
        }
    }
}