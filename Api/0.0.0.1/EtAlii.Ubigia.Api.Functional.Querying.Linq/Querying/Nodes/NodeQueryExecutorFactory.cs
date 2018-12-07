namespace EtAlii.Ubigia.Api.Functional
{
    internal class NodeQueryExecutorFactory : INodeQueryExecutorFactory
    {
        private readonly IScriptsSet _scriptsSet;

        public NodeQueryExecutorFactory(IScriptsSet scriptsSet)
        {
            _scriptsSet = scriptsSet;
        }

        public INodeQueryExecutor Create()
        {
            var scriptAggregator = new ScriptAggregator();
            var nodeQueryModelVisitor = new NodeQueryModelVisitor(scriptAggregator);
            return new NodeQueryExecutor(nodeQueryModelVisitor, _scriptsSet);
        }
    }
}