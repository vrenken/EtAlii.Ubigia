namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentStorer
    {
        void Store(in Identifier identifier, Content content);
    }
}