namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class DataContext : IDataContext
    {
        public INodeSet Nodes => _nodes;
        private readonly INodeSet _nodes;

        public IScriptsSet Scripts => _scripts;
        private readonly IScriptsSet _scripts;

        public IChangeTracker ChangeTracker => _changeTracker;
        private readonly IChangeTracker _changeTracker;

        public IIndexSet Indexes => _indexes;
        private readonly IIndexSet _indexes;

        public IDataContextConfiguration Configuration => _configuration;
        private readonly IDataContextConfiguration _configuration;

        internal DataContext(
            IDataContextConfiguration configuration,
            INodeSet nodes,
            IIndexSet indexes,
            IScriptsSet scripts,
            IChangeTracker changeTracker)
        {
            _configuration = configuration;
            _nodes = nodes;
            _scripts = scripts;
            _indexes = indexes;
            _changeTracker = changeTracker;
        }

        public void Dispose()
        {
            _changeTracker.Dispose();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

    }
}
