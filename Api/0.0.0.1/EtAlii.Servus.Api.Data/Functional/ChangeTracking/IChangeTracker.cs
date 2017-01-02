namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;

    public interface IChangeTracker : IDisposable
    {
        bool HasChanges { get; }
        IEnumerable<NodeTrackingReference> Entries { get; }
    }
}