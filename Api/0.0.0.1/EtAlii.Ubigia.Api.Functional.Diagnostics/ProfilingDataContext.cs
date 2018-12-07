namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    public class ProfilingDataContext : IProfilingDataContext
    {
        private readonly IDataContext _decoree;

        public IProfiler Profiler { get; }
        public IScriptsSet Scripts => _decoree.Scripts;
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