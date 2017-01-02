namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IBlobRetriever
    {
        T Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase;
    }
}
