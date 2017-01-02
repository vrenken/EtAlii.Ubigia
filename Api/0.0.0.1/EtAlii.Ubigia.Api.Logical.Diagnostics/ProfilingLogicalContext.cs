namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingLogicalContext : IProfilingLogicalContext
    {
        private readonly ILogicalContext _decoree;

        public IProfiler Profiler { get { return _profiler; } }
        private readonly IProfiler _profiler;

        public ILogicalContextConfiguration Configuration { get { return _decoree.Configuration; } }
        public ILogicalNodeSet Nodes { get { return _decoree.Nodes; } }
        public ILogicalRootSet Roots { get { return _decoree.Roots; } }
        public IContentManager Content { get { return _decoree.Content; } }
        public IPropertiesManager Properties { get { return _decoree.Properties; } }

        public ProfilingLogicalContext(ILogicalContext decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public void Dispose()
        {
            _decoree.Dispose();
        }
    }
}