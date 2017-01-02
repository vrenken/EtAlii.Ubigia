namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeSaveCommand : INodeSaveCommand
    {
        private readonly IScriptParserFactory _scriptParserFactory;
        private readonly IScriptProcessorFactory _scriptProcessorFactory;
        private readonly ILogicalContext _logicalContext;
        private readonly IFunctionHandlersProvider _functionHandlersProvider;
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        internal NodeSaveCommand(
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
        
        public void Execute(INode node)
        {
            if (node.IsModified)
            {
                var scriptAggregator = new ScriptAggregator();

                var scope = new ScriptScope();

                const string updateVariableName = "node";
                scope.Variables.Add(updateVariableName, new ScopeVariable(node, "updateVariableName"));
                scriptAggregator.AddUpdateItem(node.Id, updateVariableName);

                var scriptText = scriptAggregator.GetScript();

                var scriptParserConfiguration = new ScriptParserConfiguration()
                    .Use(_logicalContext.Configuration);
                    //.Use(_diagnostics)
                var scriptParser = _scriptParserFactory.Create(scriptParserConfiguration);
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
