// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    internal class ContentCacheContextProvider : IContentCacheContextProvider
    {
        public IContentContext Context { get; }

        public ContentCacheContextProvider(IContentContext context)
        {
            Context = context;
        }
    }
}
