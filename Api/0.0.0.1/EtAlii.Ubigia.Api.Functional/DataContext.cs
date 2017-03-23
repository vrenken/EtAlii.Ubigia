namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class DataContext : IDataContext
    {
        public INodeSet Nodes { get; }

        public IScriptsSet Scripts { get; }

        public IChangeTracker ChangeTracker { get; }

        public IIndexSet Indexes { get; }

        public IDataContextConfiguration Configuration { get; }

        internal DataContext(
            IDataContextConfiguration configuration,
            INodeSet nodes,
            IIndexSet indexes,
            IScriptsSet scripts,
            IChangeTracker changeTracker)
        {
            Configuration = configuration;
            Nodes = nodes;
            Scripts = scripts;
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
