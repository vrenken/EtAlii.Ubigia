namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ChangeTracker : IChangeTracker
    {
        public bool HasChanges { get; private set; }

        public IEnumerable<NodeTrackingReference> Entries
        {
            get
            {
                return _entries;
            }
        }
        private readonly List<NodeTrackingReference> _entries;

        public ChangeTracker()
        {
            _entries = new List<NodeTrackingReference>();
            HasChanges = false;
        }

        public void Dispose()
        {
        }
    }
}