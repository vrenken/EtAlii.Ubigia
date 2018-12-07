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
            ChangeTracker.Dispose();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

    }
}