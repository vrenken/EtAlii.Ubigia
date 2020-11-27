namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentSet
    {
        Task<IReadOnlyContent> Get(Identifier identifier);
        Task<IReadOnlyContentPart> Get(Identifier identifier, ulong contentPartId);
        void Store(in Identifier identifier, ContentPart contentPart);
        void Store(in Identifier identifier, Content content);
    }
}