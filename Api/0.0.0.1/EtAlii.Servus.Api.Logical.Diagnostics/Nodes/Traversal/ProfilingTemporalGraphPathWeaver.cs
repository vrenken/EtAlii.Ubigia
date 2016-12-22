namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Logical;

    public class ProfilingTemporalGraphPathWeaver : ITemporalGraphPathWeaver
    {
        private readonly ITemporalGraphPathWeaver _decoree;
        private readonly IProfiler _profiler;

        public ProfilingTemporalGraphPathWeaver(
            ITemporalGraphPathWeaver decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.TemporalWeaver);
        }

        public GraphPath Weave(GraphPath path)
        {
            dynamic profile = _profiler.Begin("Weaving temporal graph components: " + path);
            profile.Path = path.ToString();

            var result = _decoree.Weave(path);

            profile.Result = result.ToString();
            _profiler.End(profile);

            return result;
        }
    }
}