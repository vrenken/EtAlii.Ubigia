﻿namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;

    public interface IEntryCacheProvider
    {
        IDictionary<Identifier, IReadOnlyEntry> Cache { get; }
    }
}