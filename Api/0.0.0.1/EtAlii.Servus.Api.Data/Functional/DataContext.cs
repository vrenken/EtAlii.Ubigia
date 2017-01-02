namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Api.Data;
    using System;

    public class DataContext : IDisposable
    {
        public INodeSet Nodes { get { return _nodes; } }
        private readonly INodeSet _nodes;

        public IRootSet Roots { get { return _roots; } }
        private readonly IRootSet _roots;

        public IScriptParser Parser { get { return _parser; } }
        private readonly IScriptParser _parser;

        public IScriptProcessor Processor { get { return _processor; } }
        private readonly IScriptProcessor _processor;

        public IChangeTracker ChangeTracker { get { return _changeTracker; } }
        private readonly IChangeTracker _changeTracker;

        public IIndexSet Indexes { get { return _indexes; } }
        private readonly IIndexSet _indexes;

        public DataContextConfiguration Configuration
        {
            get { return _configuration; }
        }
        private readonly DataContextConfiguration _configuration;

        internal DataContext(
            INodeSet nodes,
            IRootSet roots,
            IIndexSet indexes,
            IScriptParser parser,
            IScriptProcessor processor,
            IChangeTracker changeTracker, 
            DataContextConfiguration configuration)
        {
            _nodes = nodes;
            _roots = roots;
            _indexes = indexes;
            _parser = parser;
            _processor = processor;
            _changeTracker = changeTracker;
            _configuration = configuration;
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
