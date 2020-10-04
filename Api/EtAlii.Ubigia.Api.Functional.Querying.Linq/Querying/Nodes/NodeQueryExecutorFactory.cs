namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal class NodeQueryExecutorFactory : INodeQueryExecutorFactory
    {
        private readonly IGraphSLScriptContext _scriptContext;

        public NodeQueryExecutorFactory(IGraphSLScriptContext scriptContext)
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