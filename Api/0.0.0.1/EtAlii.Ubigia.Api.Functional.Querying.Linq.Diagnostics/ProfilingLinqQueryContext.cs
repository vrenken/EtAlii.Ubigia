﻿namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
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

        public void Dispose()
        {
            _decoree.Dispose();
        }
        
        public void SaveChanges()
        {
            dynamic profile = Profiler.Begin("Saving changes");
            
            _decoree.SaveChanges();

            Profiler.End(profile);
        }
    }
}
