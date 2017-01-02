namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class EntryCacheContextProvider
    {
        public readonly IEntryContext Context;

        public EntryCacheContextProvider(EntryContext context)
        {
            Context = context;
        }
    }
}
