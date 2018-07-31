namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Structure;

    public class ProfilingDataContext : IProfilingDataContext
    {
        private readonly IDataContext _decoree;

        public IProfiler Profiler { get; }
        public IScriptsSet Scripts => _decoree.Scripts;
        public IQuerySet Queries => _decoree.Queries;
        public IDataContextConfiguration Configuration => _decoree.Configuration;

        public ProfilingDataContext(
            IDataContext decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler;
        }
    }
}