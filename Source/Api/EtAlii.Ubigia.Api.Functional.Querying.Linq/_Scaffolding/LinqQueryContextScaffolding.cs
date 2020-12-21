namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.Structure;

    internal class LinqQueryContextScaffolding : IScaffolding
    {
        private readonly LinqQueryContextConfiguration _configuration;

        public LinqQueryContextScaffolding(LinqQueryContextConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Register(Container container)
        {
            container.Register<ILinqQueryContext, LinqQueryContext>();

            container.Register(() => new TraversalScriptContextFactory().Create(_configuration));
            container.Register<IChangeTracker, ChangeTracker>();

            container.Register<IIndexSet, IndexSet>();

            container.Register<INodeSet, NodeSet>();
            container.Register<INodeQueryProviderFactory, NodeQueryProviderFactory>();
            container.Register<INodeQueryExecutorFactory, NodeQueryExecutorFactory>();

            container.Register<INodeReloadCommand, NodeReloadCommand>();
            container.Register<INodeSaveCommand, NodeSaveCommand>();

            container.Register<IRootQueryProviderFactory, RootQueryProviderFactory>();
            container.Register<IRootQueryExecutorFactory, RootQueryExecutorFactory>();

            container.Register<IQueryParser, QueryParser>(QueryParser.CreateDefault);
        }
    }
}
