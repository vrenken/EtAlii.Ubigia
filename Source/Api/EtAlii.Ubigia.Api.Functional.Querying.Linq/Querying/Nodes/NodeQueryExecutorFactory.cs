namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal class NodeQueryExecutorFactory : INodeQueryExecutorFactory
    {
        private readonly ITraversalScriptContext _scriptContext;

        public NodeQueryExecutorFactory(ITraversalScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public INodeQueryExecutor Create()
        {
            var scriptAggregator = new ScriptAggregator();
            var nodeQueryModelVisitor = new NodeQueryModelVisitor(scriptAggregator);
            return new NodeQueryExecutor(nodeQueryModelVisitor, _scriptContext);
        }
    }
}
