namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Functional;

    public class ProfilingDataContext : IProfilingDataContext
    {
        private readonly IDataContext _decoree;

        public IProfiler Profiler { get { return _profiler; } }
        private readonly IProfiler _profiler;

        public INodeSet Nodes { get { return _decoree.Nodes; } }
        public IScriptsSet Scripts { get { return _decoree.Scripts; } }
        public IChangeTracker ChangeTracker { get { return _decoree.ChangeTracker; } }
        public IIndexSet Indexes { get { return _decoree.Indexes; } }
        public IDataContextConfiguration Configuration { get { return _decoree.Configuration; } }

        public ProfilingDataContext(
            IDataContext decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public void Dispose()
        {
            _decoree.Dispose();
        }

        public void SaveChanges()
        {
            _decoree.SaveChanges();
        }
    }
}