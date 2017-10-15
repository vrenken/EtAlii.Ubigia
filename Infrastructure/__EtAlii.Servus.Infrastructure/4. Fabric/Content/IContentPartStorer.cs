namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;

    public interface IContentPartStorer
    {
        void Store(Identifier identifier, ContentPart contentPart);
    }
}