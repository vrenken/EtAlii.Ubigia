namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Servus.Api.Logical;

    internal class NodeReloadCommand : INodeReloadCommand
    {
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly ILogicalContext _logicalContext;
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        internal NodeReloadCommand(
            IScriptParserFactory scriptParserFactory,
            IScriptProcessorFactory scriptProcessorFactory,
            ILogicalContext logicalContext,
            IFunctionHandlersProvider functionHandlersProvider,
            IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _scriptParserFactory = scriptParserFactory;
            _scriptProcessorFactory = scriptProcessorFactory;
            _logicalContext = logicalContext;
            _functionHandlersProvider = functionHandlersProvider;
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
        }

        public void Execute(INode node)//, bool updateToLatest = false)
        {
            if (node.IsModified)
            {
                var scriptAggregator = new ScriptAggregator();

                var scope = new ScriptScope();

                scriptAggregator.AddGetItem(node.Id);

                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_logicalContext.Configuration);
                    //.Use(_diagnostics)
                var scriptParser = _scriptParserFactory.Create(scriptParserConfiguration);
                var scriptText = scriptAggregator.GetScript();
                var scriptParseResult = scriptParser.Parse(scriptText);

                var scriptProcessorConfiguration = new ScriptProcessorConfiguration()
                    .Use(scope)
                    .Use(_logicalContext)
                    .Use(_functionHandlersProvider)
                    .Use(_rootHandlerMappersProvider);
                var scriptProcessor = _scriptProcessorFactory.Create(scriptProcessorConfiguration);

                // TODO: Attempt to make Linq async.
                var lastSequence = scriptProcessor
                    .Process(scriptParseResult.Script)
                    .ToEnumerable()
                    .Last();
                var output = lastSequence.Output.ToEnumerable();

                UpdateOriginalNode(node, output);
            }
        }

        private void UpdateOriginalNode(INode node, IEnumerable<object> output)
        {
            var updatedNode = output.Single() as IInternalNode;
            if (updatedNode == null)
            {
                throw new InvalidOperationException("Unable to update node");
            }
            var targetNode = (IInternalNode)node;
            var properties = updatedNode.GetProperties();
            targetNode.Update(properties, updatedNode.Entry);
        }

    }
}
