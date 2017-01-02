namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ContentCacheEntry
    {
        public IReadOnlyContentDefinition ContentDefinition { get; set; }

        //public Dictionary<UInt64, IReadOnlyContentDefinitionPart> ContentDefinitionParts { get { return _contentDefinitionParts; } }
        //private readonly Dictionary<UInt64, IReadOnlyContentDefinitionPart> _contentDefinitionParts;

        public IReadOnlyContent Content { get; set; }

        public Dictionary<UInt64, IReadOnlyContentPart> ContentParts { get { return _contentParts; } }
        private readonly Dictionary<UInt64, IReadOnlyContentPart> _contentParts;

        public ContentCacheEntry()
        {
            _contentParts = new Dictionary<ulong, IReadOnlyContentPart>();
        }
    }
}
