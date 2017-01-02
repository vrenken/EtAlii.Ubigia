namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal class NodeReloadCommand
    {
        private readonly Container _container;
        private readonly IScriptParser _scriptParser;
        private readonly IScriptProcessor _scriptProcessor;
        private readonly IDataConnection _connection;

        internal NodeReloadCommand(
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

        public void Execute(INode node)//, bool updateToLatest = false)
        {
            if (node.IsModified)
            {
                var scriptAggregator = _container.GetInstance<ScriptAggregator>();

                var output = new List<object>();
                var scope = new ScriptScope(output.Add);

                scriptAggregator.AddGetItem(node.Id);

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
