﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class NodeSet : INodeSet
    {
        private readonly INodeQueryProviderFactory _nodeQueryProviderFactory;
        private readonly INodeSaveCommand _nodeSaveCommand;
        private readonly INodeReloadCommand _nodeReloadCommand; 

        protected internal NodeSet(
            INodeSaveCommand nodeSaveCommand,
            INodeReloadCommand nodeReloadCommand, 
            INodeQueryProviderFactory nodeQueryProviderFactory)
        {
            _nodeSaveCommand = nodeSaveCommand;
            _nodeReloadCommand = nodeReloadCommand;
            _nodeQueryProviderFactory = nodeQueryProviderFactory;
        }

        public IQueryable<INode> Select(string path)
        {
            var queryProvider = _nodeQueryProviderFactory.Create();
            return new NodeQueryable<INode>(queryProvider, path);
        }

        public IQueryable<INode> Select(Root root, string path)
        {
            var queryProvider = _nodeQueryProviderFactory.Create();
            return new NodeQueryable<INode>(queryProvider, root, path);
        }

        public IQueryable<INode> Select(Root root)
        {
            var queryProvider = _nodeQueryProviderFactory.Create();
            return new NodeQueryable<INode>(queryProvider, root);
        }

        public IQueryable<INode> Select(Identifier identifier)
        {
            var queryProvider = _nodeQueryProviderFactory.Create();
            return new NodeQueryable<INode>(queryProvider, identifier);
        }

        public bool IsModified(Node node)
        {
            return ((INode)node).IsModified;
        }

        public void Save(INode node)
        {
            _nodeSaveCommand.Execute(node);
        }

        public void Reload(INode node)//, bool updateToLatest = false)
        {
            _nodeReloadCommand.Execute(node);//, updateToLatest)
        }
    }
}