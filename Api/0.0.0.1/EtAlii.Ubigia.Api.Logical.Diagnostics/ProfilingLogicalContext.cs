namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    public class ProfilingLogicalContext : IProfilingLogicalContext
    {
        private readonly ILogicalContext _decoree;

        public IProfiler Profiler { get; }

        public ILogicalContextConfiguration Configuration => _decoree.Configuration;
        public ILogicalNodeSet Nodes => _decoree.Nodes;
        public ILogicalRootSet Roots => _decoree.Roots;
        public IContentManager Content => _decoree.Content;
        public IPropertiesManager Properties => _decoree.Properties;

        public ProfilingLogicalContext(ILogicalContext decoree, IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _decoree.Dispose();
        }

        ~ProfilingLogicalContext()
        {
            Dispose(false);
        }
    }
}