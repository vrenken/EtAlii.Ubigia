namespace EtAlii.Servus.Api.Data
{
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
    using System.Linq;
    using System.Reflection;

    internal class NodeSet : INodeSet
    {
        private readonly NodeQueryProviderFactory _nodeQueryProviderFactory;
        private readonly NodeSaveCommand _nodeSaveCommand;
        private readonly NodeReloadCommand _nodeReloadCommand; 

        protected internal NodeSet(
            NodeSaveCommand nodeSaveCommand,
            NodeReloadCommand nodeReloadCommand, 
            NodeQueryProviderFactory nodeQueryProviderFactory)
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
            _nodeReloadCommand.Execute(node);//, updateToLatest);
        }
    }
}