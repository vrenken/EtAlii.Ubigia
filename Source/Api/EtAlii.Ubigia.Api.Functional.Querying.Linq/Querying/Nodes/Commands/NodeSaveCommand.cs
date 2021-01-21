namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeSaveCommand : INodeSaveCommand
    {
        private readonly ITraversalContext _traversalContext;

        public NodeSaveCommand(ITraversalContext traversalContext)
        {
            _traversalContext = traversalContext;
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
            var scriptParseResult = _traversalContext.Parse(scriptText);

            // TODO: Attempt to make Linq async.
            var lastSequence = await _traversalContext.Process(scriptParseResult.Script, scope);
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
