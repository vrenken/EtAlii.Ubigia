namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal class ContentCacheEntry
    {
        public ContentDefinition ContentDefinition { get; set; }

        //public Dictionary<UInt64, ContentDefinitionPart> ContentDefinitionParts [ get [ return _contentDefinitionParts ] ]
        //private readonly Dictionary<UInt64, ContentDefinitionPart> _contentDefinitionParts

        public Content Content { get; set; }

        public Dictionary<ulong, ContentPart> ContentParts { get; }

        public ContentCacheEntry()
        {
            ContentParts = new Dictionary<ulong, ContentPart>();
        }
    }
}
