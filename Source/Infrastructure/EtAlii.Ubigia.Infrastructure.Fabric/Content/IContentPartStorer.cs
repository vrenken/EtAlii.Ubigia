namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentPartStorer
    {
        void Store(Identifier identifier, ContentPart contentPart);
    }
}