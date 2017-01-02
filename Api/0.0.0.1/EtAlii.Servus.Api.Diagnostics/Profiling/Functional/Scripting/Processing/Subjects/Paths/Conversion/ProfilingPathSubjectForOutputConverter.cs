namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;

    internal class ProfilingPathSubjectForOutputConverter : IPathSubjectForOutputConverter
    {
        private readonly IPathSubjectForOutputConverter _decoree;
        private readonly IProfiler _profiler;

        public ProfilingPathSubjectForOutputConverter(
            IPathSubjectForOutputConverter decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubjectConversion);
        }

        public void Convert(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output)
        {
            dynamic profile = _profiler.Begin("Converting path subject for output: " + pathSubject.ToString());
            profile.PathSubject = pathSubject;

            _decoree.Convert(pathSubject, scope, output);

            _profiler.End(profile);
        }
    }
}