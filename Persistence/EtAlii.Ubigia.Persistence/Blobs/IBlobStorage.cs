namespace EtAlii.Ubigia.Persistence
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IBlobStorage
    {
        void Store(ContainerIdentifier container, IBlob blob);
        void Store(ContainerIdentifier container, IBlobPart blobPart);

        T Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase;

        T Retrieve<T>(ContainerIdentifier container, UInt64 position)
            where T : BlobPartBase;
    }
}
