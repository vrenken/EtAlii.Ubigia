namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface IDataContext : IDisposable
    {
        INodeSet Nodes { get; }
        IScriptsSet Scripts { get; }
        IChangeTracker ChangeTracker { get; }
        IIndexSet Indexes { get; }
        IDataContextConfiguration Configuration { get; }
        void SaveChanges();
    }
}