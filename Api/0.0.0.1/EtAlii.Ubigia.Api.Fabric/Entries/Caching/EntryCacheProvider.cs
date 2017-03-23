namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal class EntryCacheProvider : IEntryCacheProvider
    {
        public IDictionary<Identifier, IReadOnlyEntry> Cache { get; }

        public EntryCacheProvider()
        {
            Cache = new Dictionary<Identifier, IReadOnlyEntry>(100);
        }
    }
}
