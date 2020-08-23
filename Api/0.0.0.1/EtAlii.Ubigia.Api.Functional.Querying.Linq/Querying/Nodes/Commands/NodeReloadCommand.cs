﻿namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeReloadCommand : INodeReloadCommand
    {
        private readonly IGraphSLScriptContext _scriptContext;
        
        public NodeReloadCommand(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Execute(INode node)//, bool updateToLatest = false)
        {
            if (!node.IsModified) return;
            
            var scriptAggregator = new ScriptAggregator();

            var scope = new ScriptScope();
            scriptAggregator.AddGetItem(node.Id);
            var scriptText = scriptAggregator.GetScript();
            var scriptParseResult = _scriptContext.Parse(scriptText);

            // TODO: Attempt to make Linq async.
            var lastSequence = await _scriptContext.Process(scriptParseResult.Script, scope);
            var output = lastSequence.Output.ToEnumerable();

            UpdateOriginalNode(node, output);
        }

        private void UpdateOriginalNode(INode node, IEnumerable<object> output)
        {
            if (!(output.Single() is IInternalNode updatedNode))
            {
                throw new InvalidOperationException("Unable to update node");
            }
            var targetNode = (IInternalNode)node;
            var properties = updatedNode.GetProperties();
            targetNode.Update(properties, updatedNode.Entry);
        }

    }
}
