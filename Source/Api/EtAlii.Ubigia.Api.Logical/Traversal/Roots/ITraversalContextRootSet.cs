namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface ITraversalContextRootSet
    {
        Task<Root> Get(string name);
    }
}