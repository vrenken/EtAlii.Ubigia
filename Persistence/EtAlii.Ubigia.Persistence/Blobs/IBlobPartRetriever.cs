namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobPartRetriever
    {
        T Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPartBase;
    }
}
