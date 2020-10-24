﻿namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobRetriever
    {
        T Retrieve<T>(ContainerIdentifier container)
            where T : BlobBase;
    }
}