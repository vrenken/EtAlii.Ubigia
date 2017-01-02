namespace EtAlii.Ubigia.Storage
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IBlobPartRetriever
    {
        T Retrieve<T>(ContainerIdentifier container, UInt64 position)
            where T : BlobPartBase;
    }
}
