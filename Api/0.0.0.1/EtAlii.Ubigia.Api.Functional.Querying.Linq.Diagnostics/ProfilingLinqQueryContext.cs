namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    public class ProfilingLinqQueryContext : IProfilingLinqQueryContext
    {
        private readonly ILinqQueryContext _decoree;
        public IProfiler Profiler { get; }

        public INodeSet Nodes => _decoree.Nodes;
        public IChangeTracker ChangeTracker => _decoree.ChangeTracker;
        public IIndexSet Indexes => _decoree.Indexes;
        
        public ProfilingLinqQueryContext(
            ILinqQueryContext decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptSet);
        }
        
        public void SaveChanges()
        {
            dynamic profile = Profiler.Begin("Saving changes");
            
            _decoree.SaveChanges();

            Profiler.End(profile);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                _decoree.Dispose();
            }
        }

        ~ProfilingLinqQueryContext()
        {
            Dispose(false);
        }
    }
}
