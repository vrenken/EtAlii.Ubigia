// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    internal class ContentCacheEntry
    {
        public ContentDefinition ContentDefinition { get; set; }

        public Content Content { get; set; }

        public Dictionary<ulong, ContentPart> ContentParts { get; }

        public ContentCacheEntry()
        {
            ContentParts = new Dictionary<ulong, ContentPart>();
        }
    }
}
