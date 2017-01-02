namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IBlobPartStorer
    {
        void Store(ContainerIdentifier container, IBlobPart blobPart);
    }
}
