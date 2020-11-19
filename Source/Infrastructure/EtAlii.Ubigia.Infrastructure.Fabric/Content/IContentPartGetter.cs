namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentPartGetter
    {
        Task<IReadOnlyContentPart> Get(Identifier identifier, ulong contentPartId);
    }
}