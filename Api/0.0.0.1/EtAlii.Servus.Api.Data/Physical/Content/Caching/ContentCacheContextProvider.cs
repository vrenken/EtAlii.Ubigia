namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ContentCacheContextProvider
    {
        public readonly IContentContext Context;

        public ContentCacheContextProvider(IContentContext context)
        {
            Context = context;
        }
    }
}
