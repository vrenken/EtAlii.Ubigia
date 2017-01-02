namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal class NodeSaveCommand
    {
        private readonly Container _container;
        private readonly IScriptParser _scriptParser;
        private readonly IScriptProcessor _scriptProcessor;
        private readonly IDataConnection _connection;

        internal NodeSaveCommand(
            Container container,
            IScriptParser scriptParser, 
            IScriptProcessor scriptProcessor,
            IDataConnection connection)
        {
            _container = container;
            _scriptParser = scriptParser;
            _scriptProcessor = scriptProcessor;
            _connection = connection;
        }
        
        public void Execute(INode node)
        {
            if (node.IsModified)
            {
                var scriptAggregator = _container.GetInstance<ScriptAggregator>();

                var output = new List<object>();
                var scope = new ScriptScope(output.Add);

                const string updateVariableName = "node";
                scope.Variables.Add(updateVariableName, new ScopeVariable(node, "updateVariableName"));
                scriptAggregator.AddUpdateItem(node.Id, updateVariableName);

                var scriptText = scriptAggregator.GetScript();
                var script = _scriptParser.Parse(scriptText);

                _scriptProcessor.Process(script, scope, _connection);
                
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
