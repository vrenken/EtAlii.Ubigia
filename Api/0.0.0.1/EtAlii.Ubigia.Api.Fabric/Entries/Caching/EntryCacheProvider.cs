namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal class EntryCacheProvider : IEntryCacheProvider
    {
        public IDictionary<Identifier, IReadOnlyEntry> Cache { get; }

        public EntryCacheProvider()
        {
            Cache = new ConcurrentDictionary<Identifier, IReadOnlyEntry>();
        }
    }
}
