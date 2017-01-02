namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    internal class NodeQueryExecutorFactory : INodeQueryExecutorFactory
    {
        private readonly ILogicalContext _logicalContext;
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly IFunctionHandlersProvider _functionHandlersProvider;

        public NodeQueryExecutorFactory(
            ILogicalContext logicalContext, 
            IScriptParserFactory scriptParserFactory, 
            IScriptProcessorFactory scriptProcessorFactory, 
            IFunctionHandlersProvider functionHandlersProvider)
        {
            _logicalContext = logicalContext;
            _scriptParserFactory = scriptParserFactory;
            _scriptProcessorFactory = scriptProcessorFactory;
            _functionHandlersProvider = functionHandlersProvider;
        }

        public INodeQueryExecutor Create()
        {
            var scriptAggregator = new ScriptAggregator();
            var nodeQueryModelVisitor = new NodeQueryModelVisitor(scriptAggregator);
            return new NodeQueryExecutor(_logicalContext, _scriptParserFactory, _scriptProcessorFactory, nodeQueryModelVisitor, _functionHandlersProvider);
        }
    }
}