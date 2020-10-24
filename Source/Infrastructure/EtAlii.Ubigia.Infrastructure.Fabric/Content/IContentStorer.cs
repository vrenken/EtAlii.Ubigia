namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentStorer
    {
        void Store(Identifier identifier, Content content);
    }
}