namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;

    internal class EntryCacheProvider : IEntryCacheProvider
    {
        public IDictionary<Identifier, IReadOnlyEntry> Cache { get { return _cache; } }
        private readonly IDictionary<Identifier, IReadOnlyEntry> _cache;

        public EntryCacheProvider()
        {
            _cache = new Dictionary<Identifier, IReadOnlyEntry>(100);
        }
    }
}
