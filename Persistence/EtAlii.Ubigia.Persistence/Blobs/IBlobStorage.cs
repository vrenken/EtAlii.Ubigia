namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobStorage
    {
        void Store(ContainerIdentifier container, IBlob blob);
        void Store(ContainerIdentifier container, IBlobPart blobPart);

        T Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase;

        T Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPartBase;
    }
}
