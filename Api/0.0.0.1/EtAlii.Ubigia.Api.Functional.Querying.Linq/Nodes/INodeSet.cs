namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public interface INodeSet
    {
        IQueryable<INode> Select(string path);
        IQueryable<INode> Select(Root root, string path);
        IQueryable<INode> Select(Root root);
        IQueryable<INode> Select(Identifier identifier);
        bool IsModified(Node node);
        Task Save(INode node);
        Task Reload(INode node);//, bool updateToLatest = false)
    }
}