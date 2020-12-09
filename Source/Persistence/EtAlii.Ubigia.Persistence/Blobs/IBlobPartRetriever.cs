namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    public interface IBlobPartRetriever
    {
        Task<T> Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPart;
    }
}
