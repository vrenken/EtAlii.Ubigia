﻿namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IContentCacheRetrieveHandler
    {
        Task<Content> Handle(Identifier identifier);
    }
}