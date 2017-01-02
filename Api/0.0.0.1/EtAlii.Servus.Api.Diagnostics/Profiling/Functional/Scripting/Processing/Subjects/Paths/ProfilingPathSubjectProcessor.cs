namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;

    internal class ProfilingPathSubjectProcessor : IPathSubjectProcessor
    {
        private readonly IPathSubjectProcessor _decoree;
        private readonly IProfiler _profiler;
        public ProfilingPathSubjectProcessor(
            IPathSubjectProcessor decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubject);
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("Path subject: " + subject.ToString());
            profile.Subject = subject;

            _decoree.Process(subject, scope, output);

            _profiler.End(profile);
        }
    }
}