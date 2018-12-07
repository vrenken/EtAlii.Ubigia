namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface ILinqQueryContext : IDisposable
    {
        INodeSet Nodes { get; }
        IChangeTracker ChangeTracker { get; }
        IIndexSet Indexes { get; }
        void SaveChanges();
    }
}