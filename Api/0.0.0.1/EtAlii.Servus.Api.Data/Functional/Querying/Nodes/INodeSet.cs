namespace EtAlii.Servus.Api.Data
{
    using EtAlii.xTechnology.MicroContainer;
    using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
    using Remotion.Linq.Parsing.Structure;
    using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
    using System.Linq;
    using System.Reflection;

    public interface INodeSet
    {
        IQueryable<INode> Select(string path);
        IQueryable<INode> Select(Root root, string path);
        IQueryable<INode> Select(Root root);
        IQueryable<INode> Select(Identifier identifier);
        bool IsModified(Node node);
        void Save(INode node);
        void Reload(INode node);//, bool updateToLatest = false);
    }
}