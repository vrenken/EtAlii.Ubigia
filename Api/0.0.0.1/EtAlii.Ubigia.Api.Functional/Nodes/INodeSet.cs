namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

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