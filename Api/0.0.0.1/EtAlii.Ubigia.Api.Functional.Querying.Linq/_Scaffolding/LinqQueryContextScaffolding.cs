namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.Structure;

    internal class LinqQueryContextScaffolding : IScaffolding
    {
        private readonly ILinqQueryContextConfiguration _configuration;

        public LinqQueryContextScaffolding(ILinqQueryContextConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Register(Container container)
        {
            container.Register<ILinqQueryContext, LinqQueryContext>();

            container.Register<IGraphSLScriptContext>(() =>
            {
                var configuration = new GraphSLScriptContextConfiguration()
                    .Use(_configuration.LogicalContext);
                return new GraphSLScriptContextFactory().Create(configuration);
            });
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