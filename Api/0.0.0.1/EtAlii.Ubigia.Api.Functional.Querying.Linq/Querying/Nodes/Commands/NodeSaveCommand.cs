﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeSaveCommand : INodeSaveCommand
    {
        private readonly IGraphSLScriptContext _scriptContext;

        internal NodeSaveCommand(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }
        
        public async Task Execute(INode node)
        {
            if (!node.IsModified) return;
            
            var scriptAggregator = new ScriptAggregator();

            var scope = new ScriptScope();

            const string updateVariableName = "node";
            scope.Variables.Add(updateVariableName, new ScopeVariable(node, "updateVariableName"));
            scriptAggregator.AddUpdateItem(node.Id, updateVariableName);
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
