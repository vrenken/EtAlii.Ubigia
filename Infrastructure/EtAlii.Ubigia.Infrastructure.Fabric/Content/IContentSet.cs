namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentSet
    {
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId);
        void Store(Identifier identifier, ContentPart contentPart);
        void Store(Identifier identifier, Content content);
    }
}