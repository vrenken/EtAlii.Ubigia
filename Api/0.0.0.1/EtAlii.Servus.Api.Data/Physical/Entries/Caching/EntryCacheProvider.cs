namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class EntryCacheProvider
    {
        public readonly IDictionary<Identifier, IReadOnlyEntry> Cache;

        public EntryCacheProvider()
        {
            Cache = new Dictionary<Identifier, IReadOnlyEntry>(100);
        }
    }
}
