namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobPartStorer
    {
        void Store(ContainerIdentifier container, BlobPart blobPart);
    }
}
