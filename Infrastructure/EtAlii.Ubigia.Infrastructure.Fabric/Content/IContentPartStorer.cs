namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Api;

    public interface IContentPartStorer
    {
        void Store(Identifier identifier, ContentPart contentPart);
    }
}