namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobPartStorer
    {
        void Store(ContainerIdentifier container, BlobPartBase blobPart);
    }
}
