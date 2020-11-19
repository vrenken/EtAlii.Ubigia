namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    public interface IBlobStorage
    {
        void Store(ContainerIdentifier container, IBlob blob);
        void Store(ContainerIdentifier container, IBlobPart blobPart);

        Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase;

        Task<T> Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPartBase;
    }
}
