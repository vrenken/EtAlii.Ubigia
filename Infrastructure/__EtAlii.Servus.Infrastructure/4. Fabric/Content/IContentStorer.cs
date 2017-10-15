namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IContentStorer
    {
        void Store(Identifier identifier, Content content);
    }
}