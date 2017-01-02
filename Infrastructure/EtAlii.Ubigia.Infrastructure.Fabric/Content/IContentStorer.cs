namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IContentStorer
    {
        void Store(Identifier identifier, Content content);
    }
}