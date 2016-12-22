namespace EtAlii.Servus.Api.Fabric
{
    using System;
    using System.Collections.Generic;

    internal class ContentCacheEntry
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
