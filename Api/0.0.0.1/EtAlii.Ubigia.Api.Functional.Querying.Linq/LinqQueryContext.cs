namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class LinqQueryContext : ILinqQueryContext
    {
        public INodeSet Nodes { get; }
        public IChangeTracker ChangeTracker { get; }
        public IIndexSet Indexes { get; }

        internal LinqQueryContext(
            INodeSet nodes,
            IIndexSet indexes,
            IChangeTracker changeTracker
        )
        {
            Nodes = nodes;
            Indexes = indexes;
            ChangeTracker = changeTracker;
        }
     
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            ChangeTracker.Dispose();
        }

        ~LinqQueryContext()
        {
            Dispose(false);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

    }
}