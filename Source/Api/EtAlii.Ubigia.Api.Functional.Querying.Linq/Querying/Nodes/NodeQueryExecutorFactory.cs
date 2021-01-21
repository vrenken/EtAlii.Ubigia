namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal class NodeQueryExecutorFactory : INodeQueryExecutorFactory
    {
        private readonly ITraversalContext _traversalContext;

        public NodeQueryExecutorFactory(ITraversalContext traversalContext)
        {
            _traversalContext = traversalContext;
        }

        public INodeQueryExecutor Create()
        {
            var scriptAggregator = new ScriptAggregator();
            var nodeQueryModelVisitor = new NodeQueryModelVisitor(scriptAggregator);
            return new NodeQueryExecutor(nodeQueryModelVisitor, _traversalContext);
        }
    }
}
