namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentGetter
    {
        Task<Content> Get(Identifier identifier);
    }
}