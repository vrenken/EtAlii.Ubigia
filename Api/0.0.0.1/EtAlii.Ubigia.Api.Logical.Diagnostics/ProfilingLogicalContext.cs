namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingLogicalContext : IProfilingLogicalContext
    {
        private readonly ILogicalContext _decoree;

        public IProfiler Profiler => _profiler;
        private readonly IProfiler _profiler;

        public ILogicalContextConfiguration Configuration => _decoree.Configuration;
        public ILogicalNodeSet Nodes => _decoree.Nodes;
        public ILogicalRootSet Roots => _decoree.Roots;
        public IContentManager Content => _decoree.Content;
        public IPropertiesManager Properties => _decoree.Properties;

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