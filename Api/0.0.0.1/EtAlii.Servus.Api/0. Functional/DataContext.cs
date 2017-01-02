namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class DataContext : IDataContext
    {
        public INodeSet Nodes { get { return _nodes; } }
        private readonly INodeSet _nodes;

        public IScriptsSet Scripts { get { return _scripts; } }
        private readonly IScriptsSet _scripts;

        public IChangeTracker ChangeTracker { get { return _changeTracker; } }
        private readonly IChangeTracker _changeTracker;

        public IIndexSet Indexes { get { return _indexes; } }
        private readonly IIndexSet _indexes;

        public IDataContextConfiguration Configuration { get { return _configuration; } }
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
