namespace EtAlii.Servus.Storage
{
    using System;
    using EtAlii.Servus.Api;

    public interface IBlobPartRetriever
    {
        T Retrieve<T>(ContainerIdentifier container, UInt64 position)
            where T : BlobPartBase;
    }
}
