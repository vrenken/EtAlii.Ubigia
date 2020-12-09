﻿namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    public interface IBlobStorage
    {
        void Store(ContainerIdentifier container, Blob blob);
        void Store(ContainerIdentifier container, BlobPart blobPart);

        Task<T> Retrieve<T>(ContainerIdentifier container)
            where T : Blob;

        Task<T> Retrieve<T>(ContainerIdentifier container, ulong position)
            where T : BlobPart;
    }
}
