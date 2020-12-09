namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentPartGetter
    {
        Task<ContentPart> Get(Identifier identifier, ulong contentPartId);
    }
}