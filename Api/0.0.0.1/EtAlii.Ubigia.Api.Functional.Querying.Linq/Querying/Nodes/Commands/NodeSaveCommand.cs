namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeSaveCommand : INodeSaveCommand
    {
        private readonly IGraphSLScriptContext _scriptContext;

        internal NodeSaveCommand(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
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
                var scriptParseResult = _scriptContext.Parse(scriptText);

                // TODO: Attempt to make Linq async.
                var lastSequence = _scriptContext
                    .Process(scriptParseResult.Script, scope)
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
