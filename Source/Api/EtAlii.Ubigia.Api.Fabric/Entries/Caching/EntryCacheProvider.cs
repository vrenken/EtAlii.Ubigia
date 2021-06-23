// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
