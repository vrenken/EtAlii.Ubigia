namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IBlobRetriever
    {
        T Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase;
    }
}
