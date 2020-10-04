namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.Ubigia.Api;

    public interface IBlobPartRetriever
    {
        T Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPartBase;
    }
}
