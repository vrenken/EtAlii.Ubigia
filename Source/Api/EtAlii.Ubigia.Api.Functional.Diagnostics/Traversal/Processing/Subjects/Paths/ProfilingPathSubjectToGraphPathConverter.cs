namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingPathSubjectToGraphPathConverter : IPathSubjectToGraphPathConverter
    {
        private readonly IPathSubjectToGraphPathConverter _decoree;
        private readonly IProfiler _profiler;

        public ProfilingPathSubjectToGraphPathConverter(
            IPathSubjectToGraphPathConverter decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptProcessorPathSubject);
        }

        public async Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Converting to graph path: " + pathSubject);
            profile.PathSubject = pathSubject;

            var result = await _decoree.Convert(pathSubject, scope).ConfigureAwait(false);

            _profiler.End(profile);

            return result;
        }
    }
}
