namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    public interface IBlobRetriever
    {
        Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : Blob;
    }
}
