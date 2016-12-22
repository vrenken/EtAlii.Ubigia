namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IBlobPartStorer
    {
        void Store(ContainerIdentifier container, IBlobPart blobPart);
    }
}
