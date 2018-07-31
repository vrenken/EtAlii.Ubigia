namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeReloadCommand : INodeReloadCommand
    {
        private readonly IScriptsSet _scriptsSet;
        
        internal NodeReloadCommand(IScriptsSet scriptsSet)
        {
            _scriptsSet = scriptsSet;
        }

        public void Execute(INode node)//, bool updateToLatest = false)
        {
            if (node.IsModified)
            {
                var scriptAggregator = new ScriptAggregator();

                var scope = new ScriptScope();
                scriptAggregator.AddGetItem(node.Id);
                var scriptText = scriptAggregator.GetScript();
                var scriptParseResult = _scriptsSet.Parse(scriptText);

                // TODO: Attempt to make Linq async.
                var lastSequence = _scriptsSet
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
