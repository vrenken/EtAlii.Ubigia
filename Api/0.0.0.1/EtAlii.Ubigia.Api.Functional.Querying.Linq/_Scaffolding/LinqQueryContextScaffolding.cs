namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.Structure;

    internal class LinqQueryContextScaffolding : IScaffolding
    {
        private readonly IDataContext _dataContext;

        public LinqQueryContextScaffolding(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Register(Container container)
        {
            container.Register<ILinqQueryContext, LinqQueryContext>();

            container.Register<IGraphSLScriptContext>(() => _dataContext.CreateGraphSLScriptContext());
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