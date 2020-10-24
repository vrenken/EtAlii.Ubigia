namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public interface ILogicalContentSet
    {
        IReadOnlyContent Get(Identifier identifier);
        IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId);
        void Store(Identifier identifier, ContentPart contentPart);
        void Store(Identifier identifier, Content content);
    }
}